namespace FinancialCalculationSample
{
    public record RootCalculatorInput(Root N, double Number)
    {
        public static RootCalculatorInput From(Root n, double number)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(number, nameof(number));
            return new RootCalculatorInput(n, number);
        }
    }
}
