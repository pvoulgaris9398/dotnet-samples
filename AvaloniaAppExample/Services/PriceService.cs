using AvaloniaAppExample.Models;
using DynamicData;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace AvaloniaAppExample.Services
{
    public class PriceService : IPriceService, IDisposable
    {
        private readonly IDisposable _cleanup;
        public PriceService()
        {
            var priceData = GeneratePrices().Publish();
            Prices = priceData.ToObservableChangeSet();
            _cleanup = new CompositeDisposable(priceData.Connect());

        }
        public IObservable<IChangeSet<Price>> Prices { get; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _cleanup.Dispose();
        }

        private IObservable<Price> GeneratePrices()
        {
            string[] currencyList =
                [
                "USD",
                "GBP",
                "CAD",
                "DKK",
                "DMK",
                "RUB",
                "YEN"
                ];
            var random = new Random(1);
            int counter = 0;
            Func<decimal> nextDecimal = () => Math.Round((decimal)random.Next(1, 1000) / random.Next(20, 45), 3);
            Func<string> nextCurrency = () => currencyList[random.Next(0, currencyList.Length - 1)];


            return Observable.Create<Price>(observer =>
            {
                var initial = new Price($"Security # {counter}", nextCurrency(), DateTime.Now, nextDecimal());

                var currentPrice = initial;
                observer.OnNext(initial);

                return Observable.Interval(TimeSpan.FromSeconds(2))
                    .Subscribe(_ =>
                    {
                        counter += 1;
                        var nextPrice = new Price($"Security # {counter}", nextCurrency(), DateTime.Now, nextDecimal());
                        observer.OnNext(nextPrice);
                    });
            });
        }

    }
}
