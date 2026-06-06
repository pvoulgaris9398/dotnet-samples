using AvaloniaAppExample.Models;
using AvaloniaAppExample.Services;

namespace AvaloniaAppExample.ViewModels
{
    public sealed class PriceListViewModel : ViewModelBase, IDisposable
    {
        private readonly ReadOnlyObservableCollection<Price> _items;
        private readonly IDisposable _disposable;

        public PriceListViewModel(IPriceService priceService)
        {
            ArgumentNullException.ThrowIfNull(priceService, nameof(priceService));
            _items = new([]);
            _disposable = priceService
                .Prices.Sort(SortExpressionComparer<Price>.Descending(i => i.Timestamp))
                //.ObserveOn(RxApp.MainThreadScheduler)
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
