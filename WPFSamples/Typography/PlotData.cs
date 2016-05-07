using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typography
{
    public class PlotData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public decimal X
        {
            get { return _X;  }
            set
            {
                if (_X == value) return;
                _X = value;
                var handler = PropertyChanged;
                handler?.Invoke(this, new PropertyChangedEventArgs(nameof(X)));
            }
        }
        private decimal _X;

        public decimal Y
        {
            get { return _Y; }
            set
            {
                if (_Y == value) return;
                _Y = value;
                var handler = PropertyChanged;
                handler?.Invoke(this, new PropertyChangedEventArgs(nameof(Y)));
            }
        }
        private decimal _Y;
    }
}
