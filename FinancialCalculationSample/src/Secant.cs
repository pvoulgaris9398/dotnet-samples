namespace FinancialCalculationSample
{
    internal static class Secant
    {
        /// <summary>
        /// https://en.wikipedia.org/wiki/Secant_method
        /// </summary>
        /// <param name="f"></param>
        /// <param name="x0"></param>
        /// <param name="x1"></param>
        /// <param name="iterations"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static double Solve(
            Func<double, double> f,
            double x0,
            double x1,
            int iterations = 100,
            double tolerance = 0.000001
        )
        {
            double x2 = 0;

            foreach (var _ in Enumerable.Range(0, iterations))
            {
                x2 = x1 - (f(x1) * (x1 - x0) / (f(x1) - f(x0)));

                if (
                    Math.Abs(x0 - x1) < tolerance
                    || Math.Abs((x0 / x1) - 1) < tolerance
                    || Math.Abs(f(x1)) < tolerance
                )
                {
                    return x2;
                }
                x0 = x1;
                x1 = x2;
            }
            return x2;
        }
    }
}
