using AvaloniaAppExample.Models;

namespace AvaloniaAppExample.Services
{
    public class PriceService : IPriceService, IDisposable
    {
        private readonly CompositeDisposable _cleanup;

        public PriceService()
        {
            var priceData = GeneratePrices().Publish(); // Returns an IConnectableObservable<T>
            Prices = priceData.ToObservableChangeSet();
            _cleanup = new CompositeDisposable(priceData.Connect()); // Returns an IDisposable
        }

        /// <summary>
        /// Consumers will _Subscribe_ to these events
        /// </summary>
        public IObservable<IChangeSet<Price>> Prices { get; }

        public void Dispose()
        {
            _cleanup.Dispose();
            GC.SuppressFinalize(this);
        }

        private static IObservable<Price> GeneratePrices()
        {
            string[] currencyList =
            [
                "CHF",
                "CNY",
                "CAD",
                "DKK",
                "EUR",
                "FRF",
                "GBP",
                "HKD",
                "ISK",
                "KWD",
                "MXN",
                "NZD",
                "PKR",
                "RUB",
                "SAR",
                "SEK",
                "SGD",
                "SKK",
                "TWD",
                "USD",
                "YEN",
            ];
            var random = new Random(1);
            Func<decimal> nextDecimal = () =>
                Math.Round((decimal)random.Next(1, 1000) / random.Next(20, 45), 3);
            Func<string> nextCurrency = () => currencyList[random.Next(0, currencyList.Length - 1)];

            return Observable.Create<Price>(observer =>
            {
                return Observable
                    .Interval(TimeSpan.FromSeconds(2))
                    .Subscribe(i => /* Number of times this has been called*/
                    {
                        var currencyValue = nextCurrency();
                        var decimalValue = nextDecimal();
                        Debug.WriteLine($"Currency Value: {currencyValue}");
                        Debug.WriteLine($"Decimal Value: {decimalValue}");
                        Debug.WriteLine($"Thread: {Environment.CurrentManagedThreadId}");
                        var nextPrice = new Price(
                            $"Security # {i}",
                            currencyValue,
                            DateTime.Now,
                            decimalValue
                        );
                        observer.OnNext(nextPrice);
                    });
            });
        }
    }
}
