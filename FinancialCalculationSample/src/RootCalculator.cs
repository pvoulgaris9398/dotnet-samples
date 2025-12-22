namespace FinancialCalculationSample
{
    public static class RootCalculator
    {
        public static RootCalculatorOutput Calculate(bool logOutput, RootCalculatorInput input)
        {
            /*
             *  Are there any guidelines for what a good first guess would be???
             */
            double firstApproximation = input.N;

            int roundedTo = 7;

            int iterations = 0;

            double xn = 0;
            double xnplus1 = firstApproximation;

            do
            {
                iterations++;
                if (logOutput)
                    WriteLine("**********************");
                if (logOutput)
                    WriteLine($"Iteration: {iterations}");
                if (logOutput)
                    WriteLine($"\tXn: {xn}");
                if (logOutput)
                    WriteLine($"\tXnplus1: {xnplus1}");
                xn = xnplus1;
                xnplus1 =
                    xn
                    - (
                        (Math.Pow(xn, input.N) - input.Number)
                        / (input.N * Math.Pow(xn, input.N - 1))
                    );
            } while (Math.Abs(xnplus1 - xn) > MathExtensions.Epsilon(roundedTo));

            return new RootCalculatorOutput(
                input.N,
                input.Number,
                Math.Round(xn, roundedTo),
                roundedTo,
                iterations,
                MathExtensions.Epsilon(roundedTo)
            );
        }
    }
}
