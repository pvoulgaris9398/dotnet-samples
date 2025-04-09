using System.Linq.Expressions;

namespace FinancialCalculationSample
{
    internal sealed class NewtonRaphsonMethodRunner
    {
        public static double Solve(
            Expression<Func<double, double>> f
            , Expression<Func<double, double>> df
            , double x0
            , int iterations = 100
            , double tolerance = 0.000001)
        {
            WriteLine(new string('*', 80));
            WriteLine($"Using the {nameof(NewtonRaphson)} method:");
            WriteLine($"Solve {f} with {nameof(df)} = {df}, {nameof(x0)} = {x0}");

            var root = NewtonRaphson.Solve(f.Compile(), df.Compile(), x0, iterations, tolerance);

            WriteLine($"{nameof(root)}: {root}");

            return root;

        }
    }
}
