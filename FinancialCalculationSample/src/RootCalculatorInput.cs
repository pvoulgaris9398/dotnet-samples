namespace FinancialCalculationSample
{
    internal sealed record RootCalculatorInput(Root N, double Number)
    {
        internal static RootCalculatorInput From(Root n, double number)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(number, nameof(number));
            return new RootCalculatorInput(n, number);
        }
    }
}
