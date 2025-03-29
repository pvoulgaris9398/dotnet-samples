using System.Collections;

namespace DotNetObservableExample.Models.Collections
{
    public class SlidingWindow<T> : IEnumerable<T>
    {
        public SlidingWindow(params IAccumulator<T, object>[] accumulators)
            : this((_, _) => false, accumulators) { }
        public SlidingWindow(Func<T, T, bool> removeHeadPredicate,
            params IAccumulator<T, object>[] accumulators)
        {
            RemoveHeadPredicate = removeHeadPredicate;
            Accumulators = accumulators;
        }
        private Func<T, T, bool> RemoveHeadPredicate;
        private IAccumulator<T, object>[] Accumulators { get; }
        private Queue<T> Items { get; } = new();

        public T LastAppended { get; private set; }

        public T Head => this.Items.Peek();
        public T Tail => this.Items.Count > 0 ? this.LastAppended : throw new InvalidOperationException("Collection has no elements");
        public int Count => this.Items.Count;
        public void Append(T item)
        {
            while (this.Items.Count > 0 && this.RemoveHeadPredicate(this.Head, item))
            {
                this.RemoveHead();
            }
            this.Items.Enqueue(item);
            this.LastAppended = item;
            foreach (IAccumulator<T, object> accumulator in this.Accumulators)
            {
                accumulator.Add(item);
            }
        }
        public T RemoveHead()
        {
            T head = this.Items.Dequeue();
            foreach (IAccumulator<T, object> accumulator in this.Accumulators)
            {
                accumulator.Remove(head);
            }
            return head;
        }

        public IEnumerator<T> GetEnumerator() => this.Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
