using AvaloniaAppExample.Models;
using AvaloniaAppExample.Services;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace AvaloniaAppExample.ViewModels
{
    public class PriceListViewModel : ViewModelBase, IDisposable
    {
        private readonly ReadOnlyObservableCollection<Price> _items;
        private readonly IDisposable _disposable;

        public PriceListViewModel(IPriceService priceService)
        {
            _disposable = priceService.Prices
                .Sort(SortExpressionComparer<Price>.Descending(i => i.Timestamp))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _items)
                .Subscribe();
        }

        public ReadOnlyObservableCollection<Price> Items => _items;

        public void Dispose()
        {
            _disposable.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
