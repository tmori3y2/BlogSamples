using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reactive.Disposables;
using System.Reactive.Linq;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

using MyReactivePropertyControls.Models;
using MyReactivePropertyControls.Extensions;

namespace MyReactivePropertyControls.ViewModels
{
    public class CalculatorViewModel : IDisposable
    {
        private CompositeDisposable disposables = new CompositeDisposable();
        private CalculatorModel model;

        public ReactiveProperty<bool> CanUpdateBinding { get; private set; }

        public ReactiveProperty<string> X { get; private set; }
        public ReactiveProperty<string> Y { get; private set; }
        public ReadOnlyReactiveProperty<string> Z { get; private set; }

        public ReactiveProperty<string> Decimals { get; private set; }
        public ReactiveProperty<string> UpperBound { get; private set; }

        public ReactiveProperty<bool> HasErrors { get; private set; }
        public ReactiveProperty<bool> IsDirty { get; private set; }
        public ReactiveProperty<bool> IsReadOnly { get; private set; }

        public ReactiveCommand UpdateMetaDataCommand { get; private set; }

        public CalculatorViewModel()
            : this(null)
        {

        }

        public CalculatorViewModel(CalculatorModel model)
        {
            this.model = model ?? new CalculatorModel();

            InitializeProperties();
            InitializeDebugHelper();
        }

        private void InitializeProperties()
        {
            CanUpdateBinding =
                // Initializes the property.
                new ReactiveProperty<bool>(true)
                // Disposes this property if unused.
                .AddTo(disposables);

            X =
                // Observes the dependent property.
                model.X
                .CombineLatest(CanUpdateBinding, (d, condition) => d)
                .Where(_ =>
                    CanUpdateBinding.Value)
                // Converts from decimal to string.
                .Select(d =>
                    d.Convert(model.Decimals.Value))
                .Do(s =>
                    Debug.WriteLine("CalculatorViewModel.X: {0}({1})", (object)s, (object)"Model=>ViewModel"))
                // Initializes the property.
                .ToReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables)
                // Sets validater.
                .SetValidateNotifyError(s =>
                    s.ValidateDecimal(0m, model.UpperBoundX.Value, model.Decimals.Value));

            X
                // Updates this property by the view.
                .Do(s =>
                    Debug.WriteLine("CalculatorViewModel.X: {0}({1})", (object)s, (object)"View=>ViewModel"))
                .Where(_ =>
                    CanUpdateBinding.Value && !X.HasErrors)
                // Converts from string to decimal.
                .Select(s =>
                    s.ConvertBack(model.Decimals.Value))
                // Subscribes to source and reformats target if source not changed. 
                .Subscribe(d =>
                {
                    // Subscribes to source.
                    model.X.Value = d;
                    // Reformats target if source not changed.
                    X.Value = model.X.Value.Convert(model.Decimals.Value);
                })
                // Disposes the delegate if unused.
                .AddTo(disposables);

            Y =
                // Observes the dependent property.
                model.Y
                .CombineLatest(CanUpdateBinding, (d, condition) => d)
                .Where(_ =>
                    CanUpdateBinding.Value)
                // Converts from decimal to string.
                .Select(d =>
                    d.Convert(model.Decimals.Value))
                .Do(s =>
                    Debug.WriteLine("CalculatorViewModel.Y: {0}({1})", (object)s, (object)"Model=>ViewModel"))
                // Initializes the property.
                .ToReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables)
                // Sets validater.
                .SetValidateNotifyError(s =>
                    s.ValidateDecimal(0m, model.UpperBoundY.Value, model.Decimals.Value));

            Y
                // Updates this property by the view.
                .Do(s =>
                    Debug.WriteLine("CalculatorViewModel.Y: {0}({1})", (object)s, (object)"View=>ViewModel"))
                .Where(_ =>
                    CanUpdateBinding.Value && !Y.HasErrors)
                // Converts from string to decimal.
                .Select(s =>
                    s.ConvertBack(model.Decimals.Value))
                // Subscribes to source and reformats target if source not changed. 
                .Subscribe(d =>
                {
                    // Subscribes to source.
                    model.Y.Value = d;
                    // Reformats target if source not changed.
                    Y.Value = model.Y.Value.Convert(model.Decimals.Value);
                })
                // Disposes the delegate if unused.
                .AddTo(disposables);

            Z =
                // Observes the dependent property.
                model.Z
                .CombineLatest(CanUpdateBinding, (d, condition) => d)
                .Where(_ =>
                    CanUpdateBinding.Value)
                // Converts from decimal to string.
                .Select(d =>
                    d.Convert(model.Decimals.Value))
                .Do(s =>
                    Debug.WriteLine("CalculatorViewModel.Z: {0}({1})", (object)s, (object)"Model=>ViewModel"))
                // Initializes the property.
                .ToReadOnlyReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables);

            HasErrors =
                new[]
                {
                    X.ObserveHasErrors,
                    Y.ObserveHasErrors
                }
                .CombineLatestValuesAreAllFalse()
                .Select(b =>
                    !b)
                // Initializes the property.
                .ToReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables);

            IsDirty =
                new[]
                {
                    model.X.ChangedAsObservable(false),
                    model.Y.ChangedAsObservable(false)
                }
                .CombineLatestValuesAreAllFalse()
                .Select(b =>
                    !b)
                // Initializes the property.
                .ToReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables);

            IsReadOnly =
                // Initializes the property.
                new ReactiveProperty<bool>(false)
                // Disposes this property if unused.
                .AddTo(disposables);
        }

        private void InitializeDebugHelper()
        {
            Decimals =
                // Observes the dependent property. (debug)
                model.Decimals
                // Converts from integer to string.
                .Select(i =>
                    i.Convert())
                .Do(s =>
                    Debug.WriteLine("CalculatorViewModel.Decimals: {0}({1})", (object)s, (object)"Model=>ViewModel"))
                // Initializes the property.
                .ToReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables)
                // Sets validater.
                .SetValidateNotifyError(s =>
                    s.ValidateDecimal(0m, 8m, 0));

            Decimals
                // Updates this property by the view. (debug)
                .Do(s =>
                    Debug.WriteLine("CalculatorViewModel.Decimals: {0}({1})", (object)s, (object)"View=>ViewModel"))
                .Where(_ =>
                    !Decimals.HasErrors)
                // Converts back from string to integer.
                .Select(s =>
                    s.ConvertBack())
                // Subscribes to source and reformats target if source not changed. 
                .Subscribe(i =>
                {
                    // Subscribes to source.
                    model.Decimals.Value = i;
                    // Reformats target if source not changed.
                    Decimals.Value = model.Decimals.Value.Convert();
                })
                // Disposes the delegate if unused.
                .AddTo(disposables);

            UpperBound =
                // Observes the dependent properties.
                model.UpperBound
                // Converts from decimal to string.
                .CombineLatest(model.Decimals, (d, decimals) => d.Convert(decimals))
                .Do(s =>
                    Debug.WriteLine("CalculatorViewModel.UpperBound: {0}({1})", (object)s, (object)"Model=>ViewModel"))
                // Initializes the property.
                .ToReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables)
                // Sets validater.
                .SetValidateNotifyError(s =>
                    s.ValidateDecimal(0m, 10000000000m, model.Decimals.Value));

            UpperBound
                // Updates this property by the view.
                .Do(s =>
                    Debug.WriteLine("CalculatorViewModel.UpperBound: {0}({1})", (object)s, (object)"View=>ViewModel"))
                .Where(_ =>
                    !UpperBound.HasErrors)
                // Converts from string to decimal.
                .Select(s =>
                    s.ConvertBack(model.Decimals.Value))
                // Subscribes to source and reformats target if source not changed. 
                .Subscribe(d =>
                {
                    // Subscribes to source.
                    model.UpperBound.Value = d;
                    // Reformats target if source not changed.
                    UpperBound.Value = model.UpperBound.Value.Convert(model.Decimals.Value);
                })
                // Disposes the delegate if unused.
                .AddTo(disposables);

            UpdateMetaDataCommand =
                new[]
                {
                    Decimals.ObserveHasErrors,
                    UpperBound.ObserveHasErrors,
                    CanUpdateBinding
                }
                .CombineLatestValuesAreAllFalse()
                // Initializes the command.
                .ToReactiveCommand()
                // Disposes this command if unused.
                .AddTo(disposables);

            UpdateMetaDataCommand
                .Subscribe(_ =>
                {
                    Debug.WriteLine("CalculatorViewModel.CanUpdateBinding triggered.");
                    CanUpdateBinding.Value = true;
                })
                .AddTo(disposables);
        }

        #region IDisposable Members

        public void Dispose()
        {
            disposables.Dispose();
            model.Dispose();
        }

        #endregion
    }
}
