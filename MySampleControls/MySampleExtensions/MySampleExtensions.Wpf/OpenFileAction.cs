// <copyright file="OpenFileAction.cs" company="tmori3y2.hatenablog.com">
// Copyright (c) 2016 tmori3y2.hatenablog.com. All rights reserved.
// </copyright>
// <author>tmori3y2</author>
// <date>2016/02/18</date>
// <summary>Implements the open file action class</summary>
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
    /// <summary>An open file action.</summary>
    /// <remarks>tmori3y2, 2016/02/18.</remarks>
    public class OpenFileAction : TriggerAction<DependencyObject>
    {
        /// <summary>Gets or sets the command.</summary>
        /// <value>The command.</value>
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

        /// <summary>The command property.</summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(OpenFileAction)
            , new PropertyMetadata(null));

        /// <summary>Gets or sets the filter.</summary>
        /// <value>The filter.</value>
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

        /// <summary>The filter property.</summary>
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(string), typeof(OpenFileAction)
            , new PropertyMetadata(null));

        /// <summary>Gets or sets a value indicating whether the multiselect.</summary>
        /// <value>true if multiselect, false if not.</value>
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

        /// <summary>The multiselect property.</summary>
        public static readonly DependencyProperty MultiselectProperty =
            DependencyProperty.Register("Multiselect", typeof(bool), typeof(OpenFileAction)
            , new PropertyMetadata(null));

        /// <summary>Invokes the action.</summary>
        /// <remarks>tmori3y2, 2016/02/18.</remarks>
        /// <param name="parameter">
        /// The parameter to the action. If the action does not require a parameter, the parameter may be
        /// set to a null reference.
        /// </param>
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
