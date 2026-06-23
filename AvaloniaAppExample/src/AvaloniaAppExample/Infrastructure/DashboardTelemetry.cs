namespace AvaloniaAppExample.Infrastructure
{
#pragma warning disable CA1051 // Do not declare visible instance fields
    public sealed class DashboardTelemetry
    {
        public long TotalMessages;

        public int MessagesPerSecond;

        public int QueueDepth;

        public int BatchSize;

        public int UpdatesPerSecond;
    }
#pragma warning restore CA1051 // Do not declare visible instance fields
}
