using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
    public class MyCollectionViewModel : IDisposable
    {
        private CompositeDisposable disposables = new CompositeDisposable();
        private ReentrantBlocker blocker = new ReentrantBlocker();
        public MyCollectionModel Model;

        public ReadOnlyReactiveCollection<PointViewModel> Points { get; private set; }

        public ReadOnlyReactiveProperty<string> LastErrorMessage { get; private set; }
        public ReactiveProperty<int> CurrentIndex { get; private set; }

        public ReadOnlyReactiveProperty<bool> CanExecuteCommands { get; private set; }
        public ReadOnlyReactiveProperty<bool> CanAddRow { get; private set; }
        public ReadOnlyReactiveProperty<bool> CanInsertRow { get; private set; }
        public ReadOnlyReactiveProperty<bool> CanDeleteRows { get; private set; }

        public ReactiveCommand AddRowCommand { get; private set; }
        public ReactiveCommand InsertRowAboveCommand { get; private set; }
        public ReactiveCommand InsertRowBelowCommand { get; private set; }
        public ReactiveCommand DeleteRowsCommand { get; private set; }
        public ReactiveCommand<string> ImportFileCommand { get; private set; }

        public ReactiveProperty<bool> HasErrors { get; private set; }
        public ReactiveProperty<bool> IsDirty { get; private set; }
        public ReactiveProperty<bool> IsReadOnly { get; private set; }


        public MyCollectionViewModel() : this(null) { }

        public MyCollectionViewModel(MyCollectionModel model)
        {
            this.Model = model ?? new MyCollectionModel();

            InitializeProperties();
            InitializeControlProperties();
            InitializeCommands();
        }

        private void InitializeProperties()
        {
            Points =
                // Observes the collection changed.
                Model.Points
                // Initializes the collection.
                .ToReadOnlyReactiveCollection(m => new PointViewModel(m))
                // Disposes this collection if unused.
                .AddTo(disposables);

            Points
                .ObserveAddChangedItems()
                .Subscribe(vms =>
                {
                    if (vms.Count() > 0)
                    {
                        // gets new item.
                        var current = vms[0];

                        // sets collection level validation rule.
                        current.X
                             .SetValidateNotifyError(s =>
                             {
                                 // checks block conditions.
                                 if (IsReadOnly.Value) return null;

                                 // skips condition checked by another rule.
                                 decimal? parsedValue = ConvertBackOrNull(s);
                                 if (parsedValue == null) return null;

                                 // validates duplicate values.
                                 var duplicateCount = Points
                                    // gets other items.
                                    .Where(item => item != current)
                                    // gets runtime values.
                                    .Select(item =>
                                    {
                                        // gets displayed value if the value has errors.
                                        if (item.X.HasErrors)
                                        {
                                            // gets the value that cannot be accepted but can be parsed.
                                            // gets model value if the value cannot be parsed.
                                            decimal? value = ConvertBackOrNull(item.X.Value) ?? item.Model.X.Value;
                                            return value.Value;
                                        }

                                        // gets model value.
                                        return item.Model.X.Value;
                                    })
                                    // gets same values.
                                    .Where(runTime => runTime == parsedValue.Value)
                                    .Count();

                                 string message = null;
                                 if (duplicateCount > 0) message = "Cannot set the duplicate X.";
                                 return message;
                             });
                    }
                })
                .AddTo(disposables);

            Points
                .ObserveRemoveChangedItems()
                .Where(_ => !IsReadOnly.Value)
                .Subscribe(_ =>
                {
                    if (!blocker.Blocked)
                    {
                        using (blocker.Enter())
                        {
                            // gets other items which has errors.
                            var items = Points.Where(item => item.X.HasErrors);

                            // validates items again.
                            foreach (var item in items)
                            {
                                item.X.ForceNotify();
                            }
                        }
                    }
                })
                .AddTo(disposables);

            Points
                .ObserveElementObservableProperty(item => item.X)
                .Where(_ => !IsReadOnly.Value)
                .Subscribe(pack =>
                {
                    if (!blocker.Blocked)
                    {
                        using (blocker.Enter())
                        {
                            // gets other items.
                            var items = Points.Where(item => (item != pack.Instance));

                            // validates items again.
                            foreach (var item in items)
                            {
                                item.X.ForceNotify();
                            }
                        }
                    }
                })
                .AddTo(disposables);
        }

        private void InitializeControlProperties()
        {
            HasErrors =
                // Initializes the property.
                new ReactiveProperty<bool>(false)
                // Disposes this property if unused.
                .AddTo(disposables);

            IsDirty =
                // Initializes the property.
                new ReactiveProperty<bool>(false)
                // Disposes this property if unused.
                .AddTo(disposables);

            IsDirty
                // Checks to clear dirty.
                .Where(dirty => !dirty)
                .Do(_ =>
                    Debug.WriteLine("Points viewModel is cleared dirty flag."))
                .Subscribe(_ =>
                {
                    foreach (var item in Points)
                    {
                        item.IsDirty.Value = false;
                    }
                })
                .AddTo(disposables);

            Points
                // Observes the collection changed.
                .CollectionChangedAsObservable()
                .Do(_ =>
                    Debug.WriteLine("Points viewModel changed."))
                .ChangedAsObservable(false)
                .CombineLatest(
                    Points
                        .ObserveElementObservableProperty(item => item.HasErrors)
                        .ChangedAsObservable(false),
                    (x, y) => x || y)
                .Subscribe(_ =>
                {
                    HasErrors.Value = Points
                        .Any(x => x.HasErrors.Value);
                })
                // Disposes the delegate if unused.
                .AddTo(disposables);

            Points
                // Observes the collection changed.
                .CollectionChangedAsObservable()
                .ChangedAsObservable(false)
                .Subscribe(_ =>
                {
                    IsDirty.Value = true;
                })
                // Disposes the delegate if unused.
                .AddTo(disposables);

            Points
                // Observes the item property changed.
                .ObserveElementObservableProperty(item => item.IsDirty)
                .Where(p => p.Value)
                .ChangedAsObservable(false)
                .Subscribe(_ =>
                {
                    IsDirty.Value = true;
                })
                // Disposes the delegate if unused.
                .AddTo(disposables);

            IsReadOnly =
                // Initializes the property.
                new ReactiveProperty<bool>(true)
                // Disposes this property if unused.
                .AddTo(disposables);
        }

        private void InitializeCommands()
        {
            LastErrorMessage =
                // Observes the dependent property.
                Model.LastErrorMessage
                // Initializes the property.
                .ToReadOnlyReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables);

            CurrentIndex =
                // Initializes the property.
                new ReactiveProperty<int>(-1)
                // Disposes this property if unused.
                .AddTo(disposables);

            CanExecuteCommands =
                // Observes the dependent properties.
                new[]
                {
                    (IObservable<bool>)IsReadOnly,
                    (IObservable<bool>)HasErrors
                }
                .CombineLatestValuesAreAllFalse()
                // Initializes the property.
                .ToReadOnlyReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables);

            CanAddRow =
                // Observes the dependent properties.
                new[]
                {
                    CanExecuteCommands,
                    Model.CanAddRow
                }
                .CombineLatestValuesAreAllTrue()
                // Initializes the property.
                .ToReadOnlyReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables);

            CanInsertRow =
                // Observes the dependent properties.
                new[]
                {
                    CanExecuteCommands,
                    Model.CanInsertRow
                }
                .CombineLatestValuesAreAllTrue()
                .CombineLatest(CurrentIndex, (b, i) => b && (0 <= i))
                // Initializes the property.
                .ToReadOnlyReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables);

            CanDeleteRows =
                // Observes the dependent properties.
                Model.CanDeleteRows
                .CombineLatest(IsReadOnly, (canDeleteRows, isReadOnly) => canDeleteRows && !isReadOnly)
                .CombineLatest(CurrentIndex, (b, i) => b && (0 <= i))
                // Initializes the property.
                .ToReadOnlyReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables);

            AddRowCommand =
                // Observes the dependent property.
                CanAddRow
                // Initializes the command.
                .ToReactiveCommand()
                // Disposes this command if unused.
                .AddTo(disposables);

            AddRowCommand
                .Where(_ =>
                    CanAddRow.Value)
                .Subscribe(_ =>
                {
                    Model.AddRow();
                })
                .AddTo(disposables);

            InsertRowAboveCommand =
                // Observes the dependent property.
                CanInsertRow
                // Initializes the command.
                .ToReactiveCommand()
                // Disposes this command if unused.
                .AddTo(disposables);

            InsertRowAboveCommand
                .Where(_ =>
                    CanInsertRow.Value)
                .Subscribe(_ =>
                {
                    Model.InsertRowAbove(CurrentIndex.Value);
                    CurrentIndex.Value = -1;
                })
                .AddTo(disposables);

            InsertRowBelowCommand =
                // Observes the dependent property.
                CanInsertRow
                // Initializes the command.
                .ToReactiveCommand()
                // Disposes this command if unused.
                .AddTo(disposables);

            InsertRowBelowCommand
                .Where(_ =>
                    CanInsertRow.Value)
                .Subscribe(_ =>
                {
                    Model.InsertRowBelow(CurrentIndex.Value);
                    CurrentIndex.Value = -1;
                })
                .AddTo(disposables);

            DeleteRowsCommand =
                // Observes the dependent property.
                CanDeleteRows
                // Initializes the command.
                .ToReactiveCommand()
                // Disposes this command if unused.
                .AddTo(disposables);

            DeleteRowsCommand
                .Where(_ =>
                    CanDeleteRows.Value)
                .Subscribe(_ =>
                {
                    Model.DeleteRows(CurrentIndex.Value);
                })
                .AddTo(disposables);

            ImportFileCommand =
                // Observes the dependent property.
                CanExecuteCommands
                // Initializes the command.
                .ToReactiveCommand<string>()
                // Disposes this command if unused.
                .AddTo(disposables);

            ImportFileCommand
                .Where(_ =>
                    CanExecuteCommands.Value)
                .Subscribe(s =>
                {
                    Model.ImportFile(s);
                })
                .AddTo(disposables);
        }

        public decimal? ConvertBackOrNull(string value)
        {
            NumberStyles style = NumberStyles.Number & ~NumberStyles.AllowTrailingSign;
            IFormatProvider provider = CultureInfo.CurrentUICulture;

            decimal input = decimal.Zero;
            if (!decimal.TryParse(value, style, provider, out input))
            {
                return null;
            }
            return input;
        }

        #region IDisposable Members

        public void Dispose()
        {
            disposables.Dispose();
            Model.Dispose();
        }

        #endregion
    }
}