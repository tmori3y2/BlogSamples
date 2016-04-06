using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyReactivePropertyControls.ViewModels
{
    public class ReentrantBlocker : IDisposable
    {
        private int counter;

        public ReentrantBlocker()
        {
            counter = 0;
        }

        public bool Blocked
        {
            get
            {
                return (counter > 0);
            }
        }

        public IDisposable Enter()
        {
            counter++;
            return this;
        }

        #region IDisposable Members

        public void Dispose()
        {
            counter--;
        }

        #endregion
    }
}
