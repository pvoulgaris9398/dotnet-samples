namespace FinancialCalculationSample
{
    public record Root(int Value)
    {
        public static implicit operator Root(int value) => new(value);

        public static implicit operator int(Root value) => value.Value;

        public override string ToString() =>
            this switch
            {
                { Value: 2 } => "Square Root",
                { Value: 3 } => "Cubic Root",
                _ => $"{Value}th Root",
            };
    }
}
