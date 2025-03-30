namespace FinancialCalculationSample
{
    public static class TestRootCalculator
    {
        public static RootCalculatorOutput Execute(bool logOutput, RootCalculatorInput input) => RootCalculator.Calculate(logOutput, input);

        public static void ExecuteMany(bool logOutput, IEnumerable<RootCalculatorInput> input)
        {
            foreach (var item in input)
            {
                var result = Execute(logOutput, item);

                Console.WriteLine(result.Format());
            }
        }
    }
}
