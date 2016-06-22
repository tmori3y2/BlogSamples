// <copyright file="BindingHelper.cs" company="tmori3y2.hatenablog.com">
// Copyright (c) 2016 tmori3y2.hatenablog.com. All rights reserved.
// </copyright>
// <author>tmori3y2</author>
// <date>2016/06/22</date>
// <summary>Implements the binding helper class</summary>
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
    /// <summary>A binding helper.</summary>
    /// <remarks>tmori3y2, 2016/06/22.</remarks>
    public static class BindingHelper
    {
        /// <summary>Gets binding expression.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <param name="current">The current.</param>
        /// <returns>The binding expression.</returns>
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

        /// <summary>Gets the binding expressions in this collection.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <param name="parent">The parent.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the binding expressions in this
        /// collection.
        /// </returns>
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

        /// <summary>Updates all elements described by parent.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <param name="parent">The parent.</param>
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
