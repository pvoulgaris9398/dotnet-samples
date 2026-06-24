namespace AvaloniaAppExample.Infrastructure
{
    public sealed record DashboardTelemetrySnapshot(
        long Gen0,
        long Gen1,
        long Gen2,
        long WorkingSetMb,
        long PrivateMemoryMb,
        long ManagedHeapMb,
        long TotalMessages,
        int MessagesPerSecond,
        int QueueDepth,
        int BatchSize,
        int UpdatesPerSecond
    );

#pragma warning disable CA1051 // Do not declare visible instance fields
    public sealed class DashboardTelemetry
    {
        private static readonly Process CurrentProcess = Process.GetCurrentProcess();

        private long _totalMessages;
        private int _messagesPerSecond;
        private int _queueDepth;
        private int _batchSize;
        private int _updatesPerSecond;

        public long WorkingSetMb => CurrentProcess.WorkingSet64 / 1024 / 1024;

        public long PrivateMemoryMb => CurrentProcess.PrivateMemorySize64 / 1024 / 1024;

        public long ManagedHeapMb => GC.GetTotalMemory(false) / 1024 / 1024;

        public long Gen0 => GC.CollectionCount(0);

        public long Gen1 => GC.CollectionCount(1);

        public long Gen2 => GC.CollectionCount(2);

        public long TotalMessages
        {
            get => Interlocked.Read(ref _totalMessages);
            set => Interlocked.Exchange(ref _totalMessages, value);
        }

        public int MessagesPerSecond
        {
            get => Volatile.Read(ref _messagesPerSecond);
            set => Volatile.Write(ref _messagesPerSecond, value);
        }

        public int QueueDepth
        {
            get => Volatile.Read(ref _queueDepth);
            set => Volatile.Write(ref _queueDepth, value);
        }

        public int BatchSize
        {
            get => Volatile.Read(ref _batchSize);
            set => Volatile.Write(ref _batchSize, value);
        }

        public int UpdatesPerSecond
        {
            get => Volatile.Read(ref _updatesPerSecond);
            set => Volatile.Write(ref _updatesPerSecond, value);
        }

        public long IncrementTotalMessages() => Interlocked.Increment(ref _totalMessages);

        public void UpdateMessagesPerSecond(int value) =>
            Volatile.Write(ref _messagesPerSecond, value);

        public void UpdateQueueDepth(int value) => Volatile.Write(ref _queueDepth, value);

        public void UpdateBatchSize(int value) => Volatile.Write(ref _batchSize, value);

        public void UpdateUpdatesPerSecond(int value) =>
            Volatile.Write(ref _updatesPerSecond, value);

        public DashboardTelemetrySnapshot ToSnapshot() =>
            new(
                Gen0,
                Gen1,
                Gen2,
                WorkingSetMb,
                PrivateMemoryMb,
                ManagedHeapMb,
                TotalMessages,
                MessagesPerSecond,
                QueueDepth,
                BatchSize,
                UpdatesPerSecond
            );
    }
#pragma warning restore CA1051 // Do not declare visible instance fields
}
