using System.Threading.Channels;
using AvaloniaAppExample.Infrastructure;
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

        private readonly Task _metrics;

        private readonly TimeSpan _uiRefreshInterval;

        private readonly int _maxBatchSize;

        private int _messagesThisSecond;

        private int _updatesPerSecond;

        public PriceService(TimeSpan? uiRefreshInterval = null, int maxBatchSize = 500)
        {
            _uiRefreshInterval = uiRefreshInterval ?? TimeSpan.FromMilliseconds(100);

            _maxBatchSize = maxBatchSize;

            _cache = new SourceCache<Price, string>(p => p.Security);

            Prices = _cache.Connect();

            _channel = Channel.CreateUnbounded<Price>(
                new() { SingleReader = true, SingleWriter = true }
            );

            // _channel = Channel.CreateBounded<Price>(
            //     new BoundedChannelOptions(10000)
            //     {
            //         SingleReader = true,
            //         SingleWriter = true,
            //         FullMode = BoundedChannelFullMode.DropOldest,
            //     }
            // );

            _cts = new CancellationTokenSource();

            _producer = Task.Run(() => Producer(_cts.Token));

            _consumer = Task.Run(() => Consumer(_cts.Token));

            _metrics = Task.Run(() => MetricsLoop(_cts.Token));
        }

        public IObservable<IChangeSet<Price, string>> Prices { get; }
        public DashboardTelemetry Telemetry { get; } = new();

        private async Task Producer(CancellationToken token)
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

                    _ = Interlocked.Increment(ref _messagesThisSecond);

                    _ = Interlocked.Increment(ref Telemetry.TotalMessages);
                }

                await Task.Delay(20, token).ConfigureAwait(false);
            }
        }

        private async Task Consumer(CancellationToken token)
        {
            using var timer = new PeriodicTimer(_uiRefreshInterval);
            List<Price> batch = new(_maxBatchSize);

            while (_ = await timer.WaitForNextTickAsync(token).ConfigureAwait(false))
            {
                batch.Clear();

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

                Telemetry.BatchSize = batch.Count;

                _ = Interlocked.Increment(ref _updatesPerSecond);
            }
        }

        private async Task MetricsLoop(CancellationToken token)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

            while (_ = await timer.WaitForNextTickAsync(token).ConfigureAwait(false))
            {
                Telemetry.MessagesPerSecond = Interlocked.Exchange(ref _messagesThisSecond, 0);

                Telemetry.UpdatesPerSecond = Interlocked.Exchange(ref _updatesPerSecond, 0);

                Telemetry.QueueDepth = _channel.Reader.Count;
            }
        }

        public void Dispose()
        {
            _cts.Cancel();

#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                _ = Task.WaitAll([_producer, _consumer, _metrics], TimeSpan.FromSeconds(2));
            }
            catch { }
#pragma warning restore CA1031 // Do not catch general exception types

            _cache.Dispose();

            _cts.Dispose();
        }
    }
}
