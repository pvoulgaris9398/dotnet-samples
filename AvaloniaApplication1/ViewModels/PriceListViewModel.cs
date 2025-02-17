using AvaloniaApplication1.Models;
using AvaloniaApplication1.Services;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace AvaloniaApplication1.ViewModels
{
    public class PriceListViewModel : ViewModelBase, IDisposable
    {
        private readonly ReadOnlyObservableCollection<Price> _items;
        private IDisposable _disposable;

        public PriceListViewModel(IPriceService priceService)
        {
            _disposable = priceService.Prices
                // Transform in DynamicData works like Select in
                // LINQ, it observes changes in one collection, and
                // projects it's elements to another collection.
                .Transform(x => x)
                // Filter is basically the same as .Where() operator
                // from LINQ. See all operators in DynamicData docs.
                .Filter(x => true)
                // Ensure the updates arrive on the UI thread.
                .ObserveOn(RxApp.MainThreadScheduler)
                // We .Bind() and now our mutable Items collection 
                // contains the new items and the GUI gets refreshed.
                .Bind(out _items)
                .Subscribe();
        }

        public ReadOnlyObservableCollection<Price> Items => _items;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
