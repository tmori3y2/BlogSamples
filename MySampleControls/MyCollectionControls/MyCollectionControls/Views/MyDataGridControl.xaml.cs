using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MySampleExtensions.Wpf;

using MyCollectionControls.Models;
using MyCollectionControls.ViewModels;

namespace MyCollectionControls.Views
{
    /// <summary>
    /// Interaction logic for MyDataGridControl.xaml
    /// </summary>
    public partial class MyDataGridControl : UserControl, IDisposable
    {
        private MyCollectionModel model;
        private MyCollectionViewModel viewModel;

        public MyDataGridControl()
        {
            InitializeComponent();
            model = new MyCollectionModel();
            DataContext = viewModel = new MyCollectionViewModel(model);
        }

        public bool HasErrors
        {
            get
            {
                return (viewModel != null) ?
                       (viewModel.HasErrors != null) ?
                       viewModel.HasErrors.Value : false : false;
            }
        }

        public bool IsDirty
        {
            get
            {
                return (viewModel != null) ?
                       (viewModel.IsDirty != null) ?
                       viewModel.IsDirty.Value : false : false;
            }
        }

        public void UpdateBindingSource()
        {
            BindingHelper.UpdateAllElements(this);
        }

        #region IDisposable Members

        public void Dispose()
        {
            viewModel.Dispose();
        }

        #endregion
    }
}
