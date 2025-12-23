namespace FinancialCalculationSample
{
    internal sealed record RootCalculatorOutput(
        Root N,
        double Input,
        double Result,
        int RoundedTo,
        int NumberOfIterations,
        double Epsilon
    )
#pragma warning disable IDE0055
    { }
#pragma warning restore IDE0055
}
