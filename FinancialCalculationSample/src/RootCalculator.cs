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

            /*
             * How are epsilon and the number of digits to round to
             * related, if at all???
             */
            const double epsilon = 0.0000001;
            int roundedTo = 7;

            //Console.WriteLine(Math.Pow(1))

            int iterations = 0;

            double Xn = 0;
            double Xnplus1 = firstApproximation;

            do
            {
                iterations++;
                if (logOutput) Console.WriteLine("**********************");
                if (logOutput) Console.WriteLine($"Iteration: {iterations}");
                if (logOutput) Console.WriteLine($"\tXn: {Xn}");
                if (logOutput) Console.WriteLine($"\tXnplus1: {Xnplus1}");
                Xn = Xnplus1;
                Xnplus1 = Xn - ((Math.Pow(Xn, input.N) - input.Number) / (input.N * Math.Pow(Xn, input.N - 1)));
            } while (Math.Abs(Xnplus1 - Xn) > epsilon);

            return new RootCalculatorOutput(input.N, input.Number, Math.Round(Xn, roundedTo), roundedTo, iterations, epsilon);
        }
    }
}
