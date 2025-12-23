namespace FinancialCalculationSample
{
    internal static class TestEulerMethod
    {
        /*
         * Numerical Methods for Engineers
         * Steven C. Chapra & Raymond P. Canale
         * Working through example of terminal velocity of a parachutist
         * using the Euler method
         * Note: The authors get a terminal velocity of 53,44 with t = 2
         * I get the same result, but with t = 1, I get 53.4 (note, no second digit)
         * This might be due to my rounding the output with each attempt
         * If I don't do that and just round the displayed value
         * I get the same value, regardless of the value of t
         * Note 2: For smaller values of t, more iterations are required to converge
         * to a value that doesn't change as much from iteration to iteration
         * TODO: Understand and implement epsilon-logic
         */
        internal static void Execute()
        {
            var v = CalculateTerminalVelocity(
                new GravitionalForce(9.81),
                new WindResistance(12.5),
                new Mass(68.1),
                new DeltaTime(0.5),
                new NumberOfIterations(100)
            );

            Console.WriteLine("Final Result: {0}", double.Round(v.Value, 2));
        }

        private static Velocity CalculateTerminalVelocity(
            GravitionalForce g,
            WindResistance c,
            Mass m,
            DeltaTime t,
            NumberOfIterations n
        )
        {
            var iterations = Enumerable.Range(0, n);

            Velocity v = new(0);
            var counter = 0;

            foreach (var each in iterations)
            {
                v = Calculate(g, c, m, v, t);
                Console.WriteLine("Iteration # {0}:\t{1}", ++counter, double.Round(v.Value, 2));
            }
            return v;
        }

        // TODO: Generalize to other types of problems
        private static Velocity Calculate(
            GravitionalForce g,
            WindResistance c,
            Mass m,
            Velocity v,
            DeltaTime t
        ) => new(v.Value + ((g.Value - (c.Value / m.Value * v.Value)) * t.Value));

        internal sealed record NumberOfIterations(int Value)
        {
            public static implicit operator int(NumberOfIterations n) => n.Value;

            public static explicit operator NumberOfIterations(int n) => new(n);
        }

        internal sealed record GravitionalForce(double Value)
        {
            public static implicit operator double(GravitionalForce m) => m.Value;

            public static explicit operator GravitionalForce(double m) => new(m);
        }

        internal sealed record WindResistance(double Value)
        {
            public static implicit operator double(WindResistance m) => m.Value;

            public static explicit operator WindResistance(double m) => new(m);
        }

        internal sealed record Mass(double Value)
        {
            public static implicit operator double(Mass m) => m.Value;

            public static explicit operator Mass(double m) => new(m);
        }

        internal sealed record Velocity(double Value);

        internal sealed record DeltaTime(double Value)
        {
            public static implicit operator double(DeltaTime m) => m.Value;

            public static explicit operator DeltaTime(double m) => new(m);
        }
    }
}
