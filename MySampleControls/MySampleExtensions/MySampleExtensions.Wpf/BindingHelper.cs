using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace MySampleExtensions.Wpf
{
    public static class BindingHelper
    {
        public static BindingExpression GetBindingExpression(DependencyObject current)
        {
            if (current == null)
            {
                return null;
            }

            // Gets the property enumerator.
            var propertyEnumerator = current.GetLocalValueEnumerator();
            while (propertyEnumerator.MoveNext())
            {
                // Checks whether the data is bound.
                if (BindingOperations.IsDataBound(current, propertyEnumerator.Current.Property))
                {
                    // Returns BindingExpression.
                    return propertyEnumerator.Current.Value as BindingExpression;
                }
            }
            return null;
        }

        public static IEnumerable<BindingExpression> GetBindingExpressions(DependencyObject parent)
        {
            var stack = new Stack<DependencyObject>();
            stack.Push(parent);
            while (stack.Count > 0)
            {
                // Gets the current DependencyObject.
                var current = stack.Pop();

                // Gets BindingExpression.
                var bindingExpression = GetBindingExpression(current);
                if (bindingExpression != null)
                {
                    yield return bindingExpression;
                }

                // Gets the children counts.
                int counts = VisualTreeHelper.GetChildrenCount(current);
                for (int i = 0; i < counts; ++i)
                {
                    // Gets the child.
                    var child = VisualTreeHelper.GetChild(current, i);
                    if (child is FrameworkElement)
                    {
                        stack.Push(child);
                    }
                }
            }
        }

        public static void UpdateAllElements(DependencyObject parent)
        {
            foreach (var bindingExpression in GetBindingExpressions(parent))
            {
                // Updates source.
                bindingExpression.UpdateSource();
            }
        }
    }
}
