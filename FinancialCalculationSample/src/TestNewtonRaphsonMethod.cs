namespace FinancialCalculationSample
{
    internal static class TestNewtonRaphsonMethod
    {
        public static void Run()
        {
            _ = NewtonRaphsonMethodRunner.Solve(
                f: x => Math.Cos(x)
                , df: x => -Math.Sin(x)
                , x0: 1
                );

            _ = NewtonRaphsonMethodRunner.Solve(
                f: x => Math.Sin(x)
                , df: x => Math.Cos(x)
                , x0: 0
                );

            _ = NewtonRaphsonMethodRunner.Solve(
                f: x => (2 * Math.Pow(x, 3)) + (-4 * Math.Pow(x, 2)) - 1.2
                , df: x => (6 * Math.Pow(x, 2)) + (-8 * x)
                , x0: 1
                );

        }
    }
}

