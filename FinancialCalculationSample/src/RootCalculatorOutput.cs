namespace FinancialCalculationSample
{
    public record RootCalculatorOutput(
        Root N,
        double Input,
        double Result,
        int RoundedTo,
        int NumberOfIterations,
        double Epsilon
    ) { }
}
