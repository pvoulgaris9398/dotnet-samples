namespace FinancialCalculationSample
{
    public static class MathExtensions
    {
        /*
        * Epsilon sub S = (0.5 x 10**2-n)%
        * where n = number of significant digits
        */
        public static double Epsilon(int significantFigures)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(
                significantFigures,
                nameof(significantFigures)
            );
            var exponent = 2 - significantFigures;
            return 0.5 * Math.Pow(10, exponent);
        }
    }
}
