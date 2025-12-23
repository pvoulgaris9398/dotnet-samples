namespace FinancialCalculationSample
{
    internal static class ArithmeticMeanReturn
    {
        internal static double Calculate(decimal[] returns)
        {
            int numberOfPeriods = returns.Length;
            var accumulator = 0m;

            if (numberOfPeriods == 0)
                return 0;

            foreach (var r in returns)
            {
                accumulator += r;
            }

            return (double)(accumulator / numberOfPeriods);
        }
    }
}
