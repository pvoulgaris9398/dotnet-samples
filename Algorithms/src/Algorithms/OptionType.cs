namespace Algorithms
{
    internal readonly struct OptionType<T>
    {
        private readonly T _value;
        private readonly bool _hasValue;

        private OptionType(T value, bool hasValue)
        {
            _value = value;
            _hasValue = hasValue;
        }

        public OptionType(T value) : this(value, true)
        {

        }

        public TOut Match<TOut>(Func<T, TOut> some, Func<TOut> none) =>
            _hasValue ? some(_value) : none();

        public void Match(Action<T> some, Action none)
        {
            if (_hasValue)
            {
                some(_value);
            }
            else { none(); }
        }

        public static OptionType<T> Some(T value) => new(value, true);
        public static OptionType<T> None() => default;
    }
}
