namespace FinancialCalculationSample
{
    public record RootCalculatorInput(Root N, double Number)
    {
        public RootCalculatorInput From(Root n, double number)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return new RootCalculatorInput(n, number);
        }
    }
}
