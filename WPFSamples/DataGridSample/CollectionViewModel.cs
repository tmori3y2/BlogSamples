using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridSample
{
    public class CollectionViewModel
    {
        public ObservableCollection<PlotData> Items { get; private set; } = new ObservableCollection<PlotData>(); 
    }
}
