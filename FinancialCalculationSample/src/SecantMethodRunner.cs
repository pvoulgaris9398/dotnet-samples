using System.Linq.Expressions;

namespace FinancialCalculationSample
{
    internal sealed class SecantMethodRunner
    {
        public static double Solve(
            Expression<Func<double, double>> f
            , double x0
            , double x1
            , int iterations = 100
            , double tolerance = 0.000001)
        {
            WriteLine(new string('*', 80));
            WriteLine($"Using the {nameof(Secant)} method:");
            WriteLine($"Solve {f} with {nameof(x0)} = {x0}, {nameof(x1)} = {x1}");

            var root = Secant.Solve(f.Compile(), x0, x1, iterations, tolerance);

            WriteLine($"{nameof(root)}: {root}");

            return root;

        }

    }
}
