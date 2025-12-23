using System.Collections.ObjectModel;
using Copernicus.Core.Interfaces;

namespace Copernicus.Core.Views
{
    public abstract class BaseListViewModel<T> : BaseViewModel, ICanCreateNewItem, ICanRefresh
        where T : BaseViewModel
    {
        public abstract Func<BaseListViewModel<T>, IEnumerable<T>> ItemsFactory { get; }

        public abstract Func<BaseListViewModel<T>, T> ItemFactory { get; }

        public ObservableCollection<T>? Items { get; internal set; }

        public int Count => Items?.Count ?? 0;

        #region ICanRefresh Implementation

        public virtual bool CanRefresh => true;

        public virtual bool MayRefresh => true;

        public virtual void Refresh()
        {
            Items = new ObservableCollection<T>(ItemsFactory(this));
            NotifyPropertyChanged(nameof(Items));
        }

        #endregion ICanRefresh Implementation

        #region ICanCreateNewItem Implementation

        public virtual bool CanCreate => false;

        public virtual bool MayCreate => false;

        public virtual BaseViewModel Create() => ItemFactory(this);

        #endregion ICanCreateNewItem Implementation
    }
}
