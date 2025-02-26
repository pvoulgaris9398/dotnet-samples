namespace FinancialCalculationSample
{
    public static class GeometricMeanReturn
    {
        public static double Calculate(decimal[] returns)
        {
            int numberOfPeriods = returns.Length;
            if (numberOfPeriods == 0) return 0;

            decimal accumulator = 1;

            foreach (decimal r in returns)
            {
                accumulator *= (1 + r);
            }

            return (double)Math.Pow((double)accumulator, (double)decimal.Divide(1, numberOfPeriods)) - 1;
        }
    }
}
