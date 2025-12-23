using System.Text;

namespace FinancialCalculationSample
{
    internal static class RootCalculatorOutputExtensions
    {
        internal static string Format(this RootCalculatorOutput output)
        {
            Console.WriteLine(output);
            var sb = new StringBuilder();
            _ = sb.AppendLine(new string('*', 80));
            //_ = sb.AppendLine("Calculated the {0} of {1}:", output.N, output.Input);
            //_ = sb.AppendLine(new string('*', 80));
            //_ = sb.AppendLine($"\tResult:\t\t{output.Result}");
            //_ = sb.AppendLine($"\tRoundedTo:\t{output.RoundedTo}");
            //_ = sb.AppendLine($"\tIterations:\t{output.NumberOfIterations}");
            //_ = sb.AppendLine($"\tEpsilon:\t{output.Epsilon}");
            return sb.ToString();
        }
    }
}
