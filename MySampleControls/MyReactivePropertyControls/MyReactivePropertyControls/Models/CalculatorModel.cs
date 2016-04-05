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

namespace MyReactivePropertyControls.Models
{
    public class CalculatorModel : IDisposable
    {
        private CompositeDisposable disposables = new CompositeDisposable();

        public ReactiveProperty<decimal> X { get; private set; }
        public ReactiveProperty<decimal> Y { get; private set; }
        public ReadOnlyReactiveProperty<decimal> Z { get; private set; }

        public ReactiveProperty<int> Decimals { get; private set; }
        public ReactiveProperty<decimal> UpperBound { get; private set; }
        public ReadOnlyReactiveProperty<decimal> UpperBoundX { get; private set; }
        public ReadOnlyReactiveProperty<decimal> UpperBoundY { get; private set; }

        public CalculatorModel()
        {
            InitializeProperties();
            InitializeDebugHelper();
        }

        private void InitializeProperties()
        {
            X =
                // Initializes the property.
                new ReactiveProperty<decimal>(0m)
                // Disposes this property if unused.
                .AddTo(disposables);

            Y =
                // Initializes the property.
                new ReactiveProperty<decimal>(0m)
                // Disposes this property if unused.
                .AddTo(disposables);

            Z =
                // Observes the dependent properties.
                // Synthesizes the value.
                X
                .CombineLatest(Y, (x, y) => x + y)
                // Initializes the property.
                .ToReadOnlyReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables);

            Decimals =
                // Initializes the property.
                new ReactiveProperty<int>(2)
                // Disposes this property if unused.
                .AddTo(disposables);

            UpperBound =
                // Initializes the property.
                new ReactiveProperty<decimal>(100m)
                // Disposes this property if unused.
                .AddTo(disposables);

            UpperBoundX =
                // Observes the dependent properties.
                // Synthesizes the value.
                UpperBound
                .CombineLatest(Y, (max, y) => (max > y) ? max - y : 0m)
                // Initializes the property.
                .ToReadOnlyReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables);

            UpperBoundY =
                // Observes the dependent properties.
                // Synthesizes the value.
                UpperBound
                .CombineLatest(X, (max, x) => (max > x) ? max - x : 0m)
                // Initializes the property.
                .ToReadOnlyReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables);
        }


        private void InitializeDebugHelper()
        {
#if DEBUG
            X
                // Updates this property by outer interface.
                // Updates this property by the view model.
                .Subscribe(d =>
                    Debug.WriteLine("CalculatorModel.X: {0}", d))
                // Disposes the delegate if unused.
                .AddTo(disposables);

            Y
                // Updates this property by outer interface.
                // Updates this property by the view model.
                .Subscribe(d =>
                    Debug.WriteLine("CalculatorModel.Y: {0}", d))
                // Disposes the delegate if unused.
                .AddTo(disposables);

            Z
                // Updates this property when the dependent properties changed.
                .Subscribe(d =>
                    Debug.WriteLine("CalculatorModel.Z: {0}", d))
                // Disposes the delegate if unused.
                .AddTo(disposables);

            Decimals
                // Updates this property by outer interface.
                // Updates this property by the view model.
                .Subscribe(i =>
                    Debug.WriteLine("CalculatorModel.Decimals: {0}", i))
                // Disposes the delegate if unused.
                .AddTo(disposables);

            UpperBound
                // Updates this property by outer interface.
                // Updates this property by the view model.
                .Subscribe(d =>
                    Debug.WriteLine("CalculatorModel.UpperBound: {0}", d))
                // Disposes the delegate if unused.
                .AddTo(disposables);

            UpperBoundX
                // Updates this property when the dependent properties changed.
                .Subscribe(d =>
                    Debug.WriteLine("CalculatorModel.UpperBoundX: {0}", d))
                // Disposes the delegate if unused.
                .AddTo(disposables);

            UpperBoundY
                // Updates this property when the dependent properties changed.
                .Subscribe(d =>
                    Debug.WriteLine("CalculatorModel.UpperBoundY: {0}", d))
                // Disposes the delegate if unused.
                .AddTo(disposables);
#endif
        }

        #region IDisposable Members

        public void Dispose()
        {
            disposables.Dispose();
        }

        #endregion
    }
}
