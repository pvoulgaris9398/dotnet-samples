namespace AvaloniaAppExample.Infrastructure
{
#pragma warning disable CA1051 // Do not declare visible instance fields
    public sealed class DashboardTelemetry
    {
        private static readonly Process CurrentProcess = Process.GetCurrentProcess();

        public long WorkingSetMb => CurrentProcess.WorkingSet64 / 1024 / 1024;

        public long PrivateMemoryMb => CurrentProcess.PrivateMemorySize64 / 1024 / 1024;

        public long ManagedHeapMb => GC.GetTotalMemory(false) / 1024 / 1024;

        public long Gen0 => GC.CollectionCount(0);

        public long Gen1 => GC.CollectionCount(1);

        public long Gen2 => GC.CollectionCount(2);

        public long TotalMessages;

        public int MessagesPerSecond;

        public int QueueDepth;

        public int BatchSize;

        public int UpdatesPerSecond;
    }
#pragma warning restore CA1051 // Do not declare visible instance fields
}
