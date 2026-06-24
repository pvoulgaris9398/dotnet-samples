using System.Globalization;
using Avalonia.Threading;
using AvaloniaAppExample.Models;
using AvaloniaAppExample.Services;

namespace AvaloniaAppExample.ViewModels
{
    public sealed class PriceListViewModel : ViewModelBase, IDisposable
    {
        private readonly ReadOnlyObservableCollection<Price> _items;
        private readonly IDisposable _cleanup;

        private readonly PriceService _service;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public PriceListViewModel(IPriceService priceService)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            ArgumentNullException.ThrowIfNull(priceService, nameof(priceService));
            _service = (PriceService)priceService;

#pragma warning disable CS0618 // Type or member is obsolete
            _cleanup = _service
                .Prices.Sample(TimeSpan.FromMilliseconds(250))
                .Sort(SortExpressionComparer<Price>.Descending(i => i.Timestamp))
                .ObserveOn(AvaloniaScheduler.Instance)
                .Bind(out _items)
                .Subscribe();
#pragma warning restore CS0618 // Type or member is obsolete

            var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(250) };

            timer.Tick += (_, _) =>
            {
                Metrics.Clear();

                Metrics.Add(
                    new("Gen 0", _service.Telemetry.Gen0.ToString(CultureInfo.InvariantCulture))
                );

                Metrics.Add(
                    new("Gen 1", _service.Telemetry.Gen1.ToString(CultureInfo.InvariantCulture))
                );

                Metrics.Add(
                    new("Gen 2", _service.Telemetry.Gen2.ToString(CultureInfo.InvariantCulture))
                );

                Metrics.Add(
                    new(
                        "Working Set (MB)",
                        _service.Telemetry.WorkingSetMb.ToString(CultureInfo.InvariantCulture)
                    )
                );

                Metrics.Add(
                    new(
                        "Private Memory (MB)",
                        _service.Telemetry.PrivateMemoryMb.ToString(CultureInfo.InvariantCulture)
                    )
                );

                Metrics.Add(
                    new(
                        "Managed Heap (MB)",
                        _service.Telemetry.ManagedHeapMb.ToString(CultureInfo.InvariantCulture)
                    )
                );

                Metrics.Add(
                    new(
                        "Messages/sec",
                        _service.Telemetry.MessagesPerSecond.ToString(CultureInfo.InvariantCulture)
                    )
                );
                Metrics.Add(
                    new(
                        "Queue Depth",
                        _service.Telemetry.QueueDepth.ToString(CultureInfo.InvariantCulture)
                    )
                );
                Metrics.Add(
                    new(
                        "Batch Size",
                        _service.Telemetry.BatchSize.ToString(CultureInfo.InvariantCulture)
                    )
                );
                Metrics.Add(
                    new(
                        "UI Updates/sec",
                        _service.Telemetry.UpdatesPerSecond.ToString(CultureInfo.InvariantCulture)
                    )
                );
                Metrics.Add(
                    new(
                        "Total Updates",
                        _service.Telemetry.TotalMessages.ToString(CultureInfo.InvariantCulture)
                    )
                );
            };

            timer.Start();
        }

        public ReadOnlyObservableCollection<Price> Items => _items;
        public ObservableCollection<DashboardMetric> Metrics { get; } = [];

        public void Dispose() => _cleanup.Dispose();
    }
}
