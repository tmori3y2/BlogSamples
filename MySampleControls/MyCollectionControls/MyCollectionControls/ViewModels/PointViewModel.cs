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

using MySampleExtensions;

using MyCollectionControls.Models;

namespace MyCollectionControls.ViewModels
{
    public class PointViewModel : IDisposable
    {
        private CompositeDisposable disposables = new CompositeDisposable();
        private PointModel model;

        public ReactiveProperty<string> X { get; private set; }
        public ReactiveProperty<string> Y { get; private set; }

        public ReactiveProperty<bool> HasErrors { get; private set; }
        public ReactiveProperty<bool> IsDirty { get; private set; }

        public PointViewModel() : this(null) { }

        public PointViewModel(PointModel model)
        {
            this.model = model ?? new PointModel();
            InitializeProperties();
            InitializeControlProperties();

            // Clears dirty flag.
            IsDirty.Value = false;
        }

        private void InitializeProperties()
        {
            X =
                // Observes the dependent property.
                model.X
                // Converts from decimal to string.
                .Select(d => d.Convert(2))
                .Do(s =>
                    Debug.WriteLine("PointViewModel.X changed by PointModel: {0}", (object)s))
                // Initializes the property.
                .ToReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables)
                // Sets validater.
                .SetValidateNotifyError(s =>
                    s.ValidateDecimal(0m, 500m, 2));

            X
                // Updates this property by the view or itself.
                .Do(s =>
                    Debug.WriteLine("PointViewModel.X changed: {0}", (object)s))
                .Where(_ => !X.HasErrors)
                // Converts from string to decimal.
                .Select(s => s.ConvertBack(2))
                // Subscribes to source and reformats target if source not changed. 
                .Subscribe(d =>
                {
                    model.X.Value = d;
                    X.Value = model.X.Value.Convert(2);
                })
                // Disposes the delegate if unused.
                .AddTo(disposables);

            Y =
                // Observes the dependent property.
                model.Y
                // Converts from decimal to string.
                .Select(d => d.Convert(2))
                .Do(s =>
                    Debug.WriteLine("PointViewModel.Y changed by PointModel: {0}", (object)s))
                // Initializes the property.
                .ToReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables)
                // Sets validater.
                .SetValidateNotifyError(s =>
                    s.ValidateDecimal(0m, 500m, 2));

            Y
                // Updates this property by the view or itself.
                .Do(s =>
                    Debug.WriteLine("PointViewModel.Y changed: {0}", (object)s))
                .Where(_ => !Y.HasErrors)
                // Converts from string to decimal.
                .Select(s => s.ConvertBack(2))
                // Subscribes to source and reformats target if source not changed. 
                .Subscribe(d =>
                {
                    model.Y.Value = d;
                    Y.Value = model.Y.Value.Convert(2);
                })
                // Disposes the delegate if unused.
                .AddTo(disposables);
        }

        private void InitializeControlProperties()
        {
            HasErrors =
                // Observes the dependent properties.
                new[]
                {
                    X.ObserveHasErrors,
                    Y.ObserveHasErrors
                }
                .CombineLatestValuesAreAllFalse()
                .Select(noErrors => !noErrors)
                .Do(hasErrors =>
                    Debug.WriteLine("PointViewModel has {0}errors", hasErrors ? (object)string.Empty : (object)"no "))
                // Initializes the property.
                .ToReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables)
                // Changes the property into error flag.
                .SetValidateNotifyError(hasErrors => hasErrors ? "PointViewModel has errors." : null);

            IsDirty =
                // Observes the dependent properties.
                new[]
                {
                    model.X.ChangedAsObservable(false),
                    model.Y.ChangedAsObservable(false)
                }
                .CombineLatestValuesAreAllFalse()
                .Select(notDirty => !notDirty)
                .Do(_ =>
                    Debug.WriteLine("PointViewModel is dirty."))
                // Initializes the property.
                .ToReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables);

            IsDirty
                // Updates this property by the view model collection or itself.
                .Where(dirty => !dirty)
                .Subscribe(_ =>
                    Debug.WriteLine("PointViewModel is cleared dirty flag."))
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
