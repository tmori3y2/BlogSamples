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

using MyCollectionControls.Properties;

namespace MyCollectionControls.Models
{
    public class MyCollectionModel : IDisposable
    {
        private CompositeDisposable disposables = new CompositeDisposable();

        public readonly static int MaxPoints = 20;

        public ReactiveCollection<PointModel> Points { get; private set; }
        public ReadOnlyReactiveProperty<int> PointCount { get; private set; }

        public ReactiveProperty<string> LastErrorMessage { get; private set; }
        
        public ReadOnlyReactiveProperty<bool> CanAddRow { get; private set; }
        public ReadOnlyReactiveProperty<bool> CanInsertRow { get; private set; }
        public ReadOnlyReactiveProperty<bool> CanDeleteRows { get; private set; }

        public MyCollectionModel()
        {
            InitializeProperties();
            InitializeCommands();
        }

        private void InitializeProperties()
        {
            Points =
                // Initializes the collection.
                new ReactiveCollection<PointModel>()
                // Disposes this collection if unused.
                .AddTo(disposables);

            PointCount =
                // Observes the collection changed.
                Points
                .CollectionChangedAsObservable()
                .Do(_ =>
                    Debug.WriteLine("Points model changed."))
                // Gets the collection count.
                .Select(_ => Points.Count)
                // Initializes the property.
                .ToReadOnlyReactiveProperty()
                // Disposes this property if unused.
                .AddTo(disposables);
        }

        private void InitializeCommands()
        {
            LastErrorMessage =
                // Initializes the property.
                new ReactiveProperty<string>()
                // Disposes this property if unused.
                .AddTo(disposables);

            CanAddRow =
                // Observes the dependent property.
                PointCount
                .Select(_ => CanAddRowBody())
                // Initializes the property.
                .ToReadOnlyReactiveProperty<bool>()
                // Disposes this property if unused.
                .AddTo(disposables);

            CanInsertRow =
                // Observes the dependent property.
                PointCount
                .Select(_ => CanInsertRowBody())
                // Initializes the property.
                .ToReadOnlyReactiveProperty<bool>()
                // Disposes this property if unused.
                .AddTo(disposables);

            CanDeleteRows =
                // Observes the dependent property.
                PointCount
                .Select(_ => CanDeleteRowsBody())
                // Initializes the property.
                .ToReadOnlyReactiveProperty<bool>()
                // Disposes this property if unused.
                .AddTo(disposables);
        }

        private bool CanAddRowBody()
        {
            if (Points != null && PointCount != null && LastErrorMessage != null)
            {
                return (PointCount.Value < MaxPoints);
            }
            else
            {
                return false;
            }
        }

        private bool CanInsertRowBody()
        {
            return CanAddRowBody();
        }

        private bool CanDeleteRowsBody()
        {
            if (Points != null && PointCount != null && LastErrorMessage != null)
            {
                return (0 < PointCount.Value);
            }
            else
            {
                return false;
            }
        }

        public void AddRow()
        {
            if (CanAddRowBody())
            {
                Points.AddOnScheduler(new PointModel());
                LastErrorMessage.Value = string.Empty;
            }
            else if (LastErrorMessage != null)
            {
                LastErrorMessage.Value = Resources.CannotAddRowString;
            }
        }

        public void InsertRowAbove(int index)
        {
            if (CanInsertRowBody() && (0 <= index) && (index < PointCount.Value))
            {
                Points.InsertOnScheduler(index, new PointModel());
                LastErrorMessage.Value = string.Empty;
            }
            else if (LastErrorMessage != null)
            {
                LastErrorMessage.Value = Resources.CannotInsertRowString;
            }
        }

        public void InsertRowBelow(int index)
        {
            if (CanInsertRowBody() && (0 <= index) && (index < PointCount.Value))
            {
                Points.InsertOnScheduler(index + 1, new PointModel());
                LastErrorMessage.Value = string.Empty;
            }
            else if (LastErrorMessage != null)
            {
                LastErrorMessage.Value = Resources.CannotInsertRowString;
            }
        }

        public void DeleteRows(int index)
        {
            if (CanDeleteRowsBody() && (0 <= index) && (index < PointCount.Value))
            {
                Points.RemoveAtOnScheduler(index);
                LastErrorMessage.Value = string.Empty;
            }
            else if (LastErrorMessage != null)
            {
                LastErrorMessage.Value = Resources.CannotDeleteRowsString;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            disposables.Dispose();
        }

        #endregion
    }
}
