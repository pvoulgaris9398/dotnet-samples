namespace FinancialCalculationSample
{
    internal static class NewtonRaphson
    {
        /// <summary>
        /// https://en.wikipedia.org/wiki/Newton%27s_method
        /// </summary>
        /// <param name="f"></param>
        /// <param name="df"></param>
        /// <param name="x0"></param>
        /// <param name="iterations"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        internal static double Solve(
            Func<double, double> f,
            Func<double, double> df,
            double x0,
            int iterations = 100,
            double tolerance = 0.000001
        )
        {
            double x = x0;
            double xnew = 0;

            foreach (var _ in Enumerable.Range(0, iterations))
            {
                xnew = x - (f(x) / df(x));

                if (Math.Abs(xnew - x) < tolerance)
                {
                    return xnew;
                }
                x = xnew;
            }
            return xnew;
        }
    }
}
