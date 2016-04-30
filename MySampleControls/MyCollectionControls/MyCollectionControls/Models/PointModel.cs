using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reactive.Disposables;
using System.Reactive.Linq;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MyCollectionControls.Models
{
    public class PointModel : IDisposable
    {
        private CompositeDisposable disposables = new CompositeDisposable();

        public ReactiveProperty<decimal> X { get; private set; }
        public ReactiveProperty<decimal> Y { get; private set; }

        public PointModel()
        {
            InitializeProperties();
        }

        public PointModel(PointModel model)
            : this()
        {
            if (model != null && model.X != null && model.Y != null)
            {
                X.Value = model.X.Value;
                Y.Value = model.Y.Value;
            }
        }

        private void InitializeProperties()
        {
            X =
                // Initializes the property.
                new ReactiveProperty<decimal>(0m)
                // Disposes this property if unused.
                .AddTo(disposables);

            X
                // Updates this property by outer interface.
                // Updates this property by the view model.
                .Subscribe(d =>
                    Debug.WriteLine("PointModel.X changed: {0:F2}", d))
                // Disposes the delegate if unused.
                .AddTo(disposables);

            Y =
                // Initializes the property.
                new ReactiveProperty<decimal>(0m)
                // Disposes this property if unused.
                .AddTo(disposables);

            Y
                // Updates this property by outer interface.
                // Updates this property by the view model.
                .Subscribe(d =>
                    Debug.WriteLine("PointModel.Y changed: {0:F2}", d))
                // Disposes the delegate if unused.
                .AddTo(disposables);
        }


        #region IDisposable Members

        public void Dispose()
        {
            disposables.Dispose();
        }

        #endregion
    }
}
