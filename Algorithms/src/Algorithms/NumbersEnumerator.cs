using System.Collections;

namespace Algorithms
{
    internal sealed class NumbersSequenceGenerator(int n) : IEnumerable<int>
    {
        internal static IEnumerable<int> GetNumbersSequence(int n) => new NumbersSequenceGenerator(n);
        public IEnumerator<int> GetEnumerator() => new NumbersEnumerator(n);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal sealed class NumbersEnumerator(int n) : IEnumerator<int>
    {
        public int Current { get; private set; } = 0;
        private int _state = 0;

        object IEnumerator.Current => Current;

        public void Dispose() => throw new NotImplementedException();

        public bool MoveNext()
        {
            (_state, Current, bool result) = _state switch
            {
                0 when Current < n => (1, Current, true),
                0 => (2, 0, false),
                1 when ++Current < n => (1, Current, true),
                1 => (2, Current, false),
                _ => (2, Current, false)
            };
            return result;
        }

        public void Reset() => (_state, Current) = (0, 0);
    }
}
