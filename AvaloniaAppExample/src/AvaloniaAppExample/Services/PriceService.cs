using System.Threading.Channels;
using AvaloniaAppExample.Models;

namespace AvaloniaAppExample.Services
{
    public sealed class PriceService : IPriceService, IDisposable
    {
        private readonly SourceCache<Price, string> _cache;

        private readonly Channel<Price> _channel;

        private readonly CancellationTokenSource _cts;

        private readonly Task _producer;

        private readonly Task _consumer;

        private readonly TimeSpan _uiRefreshInterval;

        private readonly int _maxBatchSize;

        public PriceService(TimeSpan? uiRefreshInterval = null, int maxBatchSize = 500)
        {
            _uiRefreshInterval = uiRefreshInterval ?? TimeSpan.FromMilliseconds(100);

            _maxBatchSize = maxBatchSize;

            _cache = new SourceCache<Price, string>(p => p.Security);

            Prices = _cache.Connect();

            _channel = Channel.CreateUnbounded<Price>(
                new UnboundedChannelOptions { SingleReader = true, SingleWriter = true }
            );

            _cts = new CancellationTokenSource();

            _producer = Task.Run(() => ProducePrices(_cts.Token));

            _consumer = Task.Run(() => ConsumePrices(_cts.Token));
        }

        public IObservable<IChangeSet<Price, string>> Prices { get; }

        private async Task ProducePrices(CancellationToken token)
        {
            string[] currencies = ["USD", "EUR", "GBP", "JPY", "CAD", "CHF", "SEK"];

            var symbols = Enumerable.Range(1, 500).Select(i => $"SEC-{i:000}").ToArray();

            while (!token.IsCancellationRequested)
            {
                for (int i = 0; i < 200; i++)
                {
                    var symbol = symbols[Random.Shared.Next(symbols.Length)];

                    var price = new Price(
                        symbol,
                        currencies[Random.Shared.Next(currencies.Length)],
                        DateTime.UtcNow,
                        Math.Round((decimal)Random.Shared.NextDouble() * 1000m, 2)
                    );

                    await _channel.Writer.WriteAsync(price, token).ConfigureAwait(false);
                }

                await Task.Delay(20, token).ConfigureAwait(false);
            }
        }

        private async Task ConsumePrices(CancellationToken token)
        {
            List<Price> batch = new(_maxBatchSize);

            var timer = new PeriodicTimer(_uiRefreshInterval);

            while (!token.IsCancellationRequested)
            {
                _ = await timer.WaitForNextTickAsync(token).ConfigureAwait(false);

                while (batch.Count < _maxBatchSize && _channel.Reader.TryRead(out var price))
                {
                    batch.Add(price);
                }

                if (batch.Count == 0)
                {
                    continue;
                }

                _cache.Edit(cache =>
                {
                    foreach (var p in batch)
                    {
                        cache.AddOrUpdate(p);
                    }
                });

                batch.Clear();
            }
        }

        public void Dispose()
        {
            _cts.Cancel();

            _cache.Dispose();

            _cts.Dispose();
        }
    }
}
