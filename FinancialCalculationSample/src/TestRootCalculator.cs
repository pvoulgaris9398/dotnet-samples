namespace FinancialCalculationSample
{
    internal static class TestRootCalculator
    {
        internal static RootCalculatorOutput Execute(bool logOutput, RootCalculatorInput input) =>
            RootCalculator.Calculate(logOutput, input);

        internal static void ExecuteMany(bool logOutput, IEnumerable<RootCalculatorInput> input)
        {
            foreach (var item in input)
            {
                var result = Execute(logOutput, item);

                Console.WriteLine(result.Format());
            }
        }
    }
}
