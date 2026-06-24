using System.Globalization;
using Avalonia.Threading;
using AvaloniaAppExample.Infrastructure;
using AvaloniaAppExample.Models;
using AvaloniaAppExample.Services;

namespace AvaloniaAppExample.ViewModels
{
    public sealed class PriceListViewModel : ViewModelBase, IDisposable
    {
        private readonly ReadOnlyObservableCollection<Price> _items;
        private readonly IDisposable _cleanup;
        private readonly IDisposable _telemetryCleanup;

        private readonly IPriceService _service;
        private readonly DispatcherTimer _metricsTimer;
        private ObservableCollection<DashboardMetric> _metrics = [];

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public PriceListViewModel(IPriceService priceService)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            ArgumentNullException.ThrowIfNull(priceService, nameof(priceService));
            _service = priceService;

#pragma warning disable CS0618 // Type or member is obsolete
            _cleanup = _service
                .Prices.Sample(TimeSpan.FromMilliseconds(250))
                .Sort(SortExpressionComparer<Price>.Descending(i => i.Timestamp))
                .ObserveOn(AvaloniaScheduler.Instance)
                .Bind(out _items)
                .Subscribe();
#pragma warning restore CS0618 // Type or member is obsolete

            _telemetryCleanup = _service
                .TelemetryUpdates.ObserveOn(AvaloniaScheduler.Instance)
                .Subscribe(UpdateMetrics);

            _metricsTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _metricsTimer.Tick += (_, _) => UpdateMetricsFromService();
            _metricsTimer.Start();
        }

        public ReadOnlyObservableCollection<Price> Items => _items;
        public ObservableCollection<DashboardMetric> Metrics
        {
            get => _metrics;
            private set => this.RaiseAndSetIfChanged(ref _metrics, value);
        }

        public void Dispose()
        {
            _metricsTimer.Stop();
            _metricsTimer.Tick -= (_, _) => UpdateMetricsFromService();
            _cleanup.Dispose();
            _telemetryCleanup.Dispose();
        }

        private void UpdateMetrics(DashboardTelemetrySnapshot telemetry)
        {
            Dispatcher.UIThread.Post(() =>
            {
                Metrics.Clear();

                Metrics.Add(new("Gen 0", telemetry.Gen0.ToString(CultureInfo.InvariantCulture)));
                Metrics.Add(new("Gen 1", telemetry.Gen1.ToString(CultureInfo.InvariantCulture)));
                Metrics.Add(new("Gen 2", telemetry.Gen2.ToString(CultureInfo.InvariantCulture)));
                Metrics.Add(
                    new(
                        "Working Set (MB)",
                        telemetry.WorkingSetMb.ToString(CultureInfo.InvariantCulture)
                    )
                );
                Metrics.Add(
                    new(
                        "Private Memory (MB)",
                        telemetry.PrivateMemoryMb.ToString(CultureInfo.InvariantCulture)
                    )
                );
                Metrics.Add(
                    new(
                        "Managed Heap (MB)",
                        telemetry.ManagedHeapMb.ToString(CultureInfo.InvariantCulture)
                    )
                );
                Metrics.Add(
                    new(
                        "Messages/sec",
                        telemetry.MessagesPerSecond.ToString(CultureInfo.InvariantCulture)
                    )
                );
                Metrics.Add(
                    new("Queue Depth", telemetry.QueueDepth.ToString(CultureInfo.InvariantCulture))
                );
                Metrics.Add(
                    new("Batch Size", telemetry.BatchSize.ToString(CultureInfo.InvariantCulture))
                );
                Metrics.Add(
                    new(
                        "UI Updates/sec",
                        telemetry.UpdatesPerSecond.ToString(CultureInfo.InvariantCulture)
                    )
                );
                Metrics.Add(
                    new(
                        "Total Updates",
                        telemetry.TotalMessages.ToString(CultureInfo.InvariantCulture)
                    )
                );

                this.RaisePropertyChanged(nameof(Metrics));
            });
        }

        private void UpdateMetricsFromService()
        {
            if (_service is not PriceService priceService)
            {
                return;
            }

            UpdateMetrics(priceService.Telemetry.ToSnapshot());
        }
    }
}
