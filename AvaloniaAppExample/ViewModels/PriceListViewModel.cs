using AvaloniaAppExample.Models;
using AvaloniaAppExample.Services;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace AvaloniaAppExample.ViewModels
{
    public class PriceListViewModel : ViewModelBase, IDisposable
    {
        private readonly ReadOnlyObservableCollection<Price> _items;
        private IDisposable _disposable;

        public PriceListViewModel(IPriceService priceService)
        {
            _disposable = priceService.Prices
                //.Transform(x => x)
                //.Filter(x => true)
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
