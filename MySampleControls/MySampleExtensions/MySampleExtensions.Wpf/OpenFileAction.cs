using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

using System.Windows.Interactivity;

using Microsoft.Win32;

namespace MySampleExtensions.Wpf
{
    public class OpenFileAction : TriggerAction<DependencyObject>
    {
        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(OpenFileAction)
            , new PropertyMetadata(null));

        public string Filter
        {
            get
            {
                return (string)GetValue(FilterProperty);
            }
            set
            {
                SetValue(FilterProperty, value);
            }
        }

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(string), typeof(OpenFileAction)
            , new PropertyMetadata(null));

        public bool Multiselect
        {
            get
            {
                return (bool)GetValue(MultiselectProperty);
            }
            set
            {
                SetValue(MultiselectProperty, value);
            }
        }

        public static readonly DependencyProperty MultiselectProperty =
            DependencyProperty.Register("Multiselect", typeof(bool), typeof(OpenFileAction)
            , new PropertyMetadata(null));

        protected override void Invoke(object parameter)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = !string.IsNullOrEmpty(Filter) ? Filter : "All Files (*.*)|*.*";
            openFileDialog.Multiselect = Multiselect;
            openFileDialog.ValidateNames = true;

            var result = openFileDialog.ShowDialog();
            if ((result != null) && (result == true))
            {
                var fileName = openFileDialog.FileName;
                var fileNames = openFileDialog.FileNames;
                var command = Command;
                if (command != null)
                {
                    if (!Multiselect && !string.IsNullOrEmpty(fileName))
                    {
                        command.Execute(fileName);
                    }
                    else if (Multiselect && (fileNames != null) && (fileNames.Count() > 0))
                    {
                        command.Execute(fileNames.ToList());
                    }
                }
            }
        }
    }
}
