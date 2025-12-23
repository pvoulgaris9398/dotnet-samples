namespace FinancialCalculationSample
{
    internal static class CovarianceMatrix
    {
        internal static double[,] Calculate(double[,] matrix, double[] means)
        {
            int rows = matrix.GetLength(0);

            //TODO
            Console.WriteLine(means);
            Console.WriteLine(rows);

            double[,] cov =
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
            };

            return cov;
        }
    }
}
