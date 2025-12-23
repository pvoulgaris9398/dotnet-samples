namespace FinancialCalculationSample
{
    internal static class TestSecantMethod
    {
        internal static void Run()
        {
            _ = SecantMethodRunner.Solve(f: x => Math.Cos(x), x0: 0, x1: 1);

            _ = SecantMethodRunner.Solve(f: x => Math.Sin(x), x0: 1, x1: 2);

            _ = SecantMethodRunner.Solve(f: x => Math.Log10(x) - 1.2, x0: 1, x1: 10);

            _ = SecantMethodRunner.Solve(
                f: x => (5 * Math.Pow(x, 3)) + (-6 * Math.Pow(x, 2)) - 1.2,
                x0: 1,
                x1: 10
            );
        }
    }
}
