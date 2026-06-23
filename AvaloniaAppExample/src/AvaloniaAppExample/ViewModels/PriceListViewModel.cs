using AvaloniaAppExample.Models;
using AvaloniaAppExample.Services;

namespace AvaloniaAppExample.ViewModels
{
    public sealed class PriceListViewModel : ViewModelBase, IDisposable
    {
        private readonly ReadOnlyObservableCollection<Price> _items;
        private readonly IDisposable _disposable;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public PriceListViewModel(IPriceService priceService)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            ArgumentNullException.ThrowIfNull(priceService, nameof(priceService));
#pragma warning disable CS0618 // Type or member is obsolete
            _disposable = priceService
                .Prices.Sort(SortExpressionComparer<Price>.Descending(i => i.Timestamp))
                .ObserveOn(AvaloniaScheduler.Instance)
                .Bind(out _items)
                .Subscribe();
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public ReadOnlyObservableCollection<Price> Items => _items;

        public void Dispose()
        {
            _disposable.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
