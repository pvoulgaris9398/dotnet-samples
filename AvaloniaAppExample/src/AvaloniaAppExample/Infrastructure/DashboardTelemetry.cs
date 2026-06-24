namespace AvaloniaAppExample.Infrastructure
{
#pragma warning disable CA1051 // Do not declare visible instance fields
    public sealed class DashboardTelemetry
    {
        private static readonly Process CurrentProcess = Process.GetCurrentProcess();

        public long WorkingSetMb => CurrentProcess.WorkingSet64 / 1024 / 1024;

        public long PrivateMemoryMb => CurrentProcess.PrivateMemorySize64 / 1024 / 1024;

        public long ManagedHeapMb => GC.GetTotalMemory(false) / 1024 / 1024;

        public long TotalMessages;

        public int MessagesPerSecond;

        public int QueueDepth;

        public int BatchSize;

        public int UpdatesPerSecond;
    }
#pragma warning restore CA1051 // Do not declare visible instance fields
}
