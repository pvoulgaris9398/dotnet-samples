namespace DotNetObservableExample.Models.Collections
{
    public class WorkItemsTimeWindow
    {
        public WorkItemsTimeWindow(TimeSpan interval
            )
        {
            Interval = interval;
            MoneySum = new DelegatingAccumulator<WorkItem, MoneyBag>(MoneyBag.Empty,
               (sum, item) => sum.Add(item.Cost),
               (sum, item) => sum.Subtract(item.Cost));
            this.Window = new(this.ShouldRemoveHead, this.MoneySum);
        }

        private TimeSpan Interval { get; }
        private SlidingWindow<WorkItem> Window { get; }
        private IAccumulator<WorkItem, MoneyBag> MoneySum { get; }

        public void Add(WorkItem item) => throw new NotImplementedException();
        public void SetCurrentTime(DateTime time) => throw new NotImplementedException();
        public int Count => throw new NotImplementedException();
        public MoneyBag TotalCost => throw new NotImplementedException();
        public WorkItem Head => throw new NotImplementedException();
        public WorkItem Tail => throw new NotImplementedException();
        private bool ShouldRemoveHead(WorkItem head, WorkItem tail) => throw new NotImplementedException();

    }
}
