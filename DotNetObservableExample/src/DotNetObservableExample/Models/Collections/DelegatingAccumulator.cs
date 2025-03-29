namespace DotNetObservableExample.Models.Collections
{
    public class DelegatingAccumulator<TItem, TValue> : IAccumulator<TItem, TValue>
    {
        public DelegatingAccumulator(
            TValue seed,
            Func<TValue, TItem, TValue> add,
            Func<TValue, TItem, TValue> remove)
        {
            Value = seed;
            AddStrategy = add;
            RemoveStrategy = remove;
        }

        public TValue Value { get; private set; }

        public void Add(TItem item) => Value = AddStrategy(Value, item);

        public void Remove(TItem item) => Value = RemoveStrategy(Value, item);

        private Func<TValue, TItem, TValue> AddStrategy { get; }
        private Func<TValue, TItem, TValue> RemoveStrategy { get; }
    }
}
