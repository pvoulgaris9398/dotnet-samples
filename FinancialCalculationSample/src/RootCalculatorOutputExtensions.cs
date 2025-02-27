using System.Text;

namespace FinancialCalculationSample
{
    public static class RootCalculatorOutputExtensions
    {
        public static string Format(this RootCalculatorOutput output)
        {
            var sb = new StringBuilder();
            sb.AppendLine(new string('*', 80));
            sb.AppendLine($"Calculated the {output.N} of {output.Input}:");
            sb.AppendLine(new string('*', 80));
            sb.AppendLine($"\tResult:\t\t{output.Result}");
            sb.AppendLine($"\tRoundedTo:\t{output.RoundedTo}");
            sb.AppendLine($"\tIterations:\t{output.NumberOfIterations}");
            sb.AppendLine($"\tEpsilon:\t{output.Epsilon}");
            return sb.ToString();
        }
    }
}
