using System.Globalization;
using System.Text.RegularExpressions;

namespace Algorithms
{
    internal static class RegularExpressions
    {

        /// <summary>
        /// Nice Regex samples from Daniel Hardt's blog post:
        /// https://hardt.software/f-a-simple-math-formula-solver/
        /// </summary>
        internal static void Run3()
        {
            //var mathPairRegex = @"(?<MathSymbol>[\\+\\-\\*\\/\\^])(?<Number>[\\-]*[0-9.]*)";
            //var allowedFormula = @"[0-9.\+\-\*\/\^ \(\)%]";
            //var isMathSymbol = @"[\+\-\*\/\^\(\)]";
            //var brackets = @"\((?<InsideBrackets>[0-9.\+\-\*\/\^]*)\)";
            //var multiplyDivide = @"(?<MultiplyDivide>[\+\-][0-9.]*[\*\/]{1}[\-]{0,1}[0-9.]*)";
            string power = @"(?<Power>[\+\-\*\/\^][0-9.]*[\^]{1}[\-]{0,1}[0-9.]*)";

            string input = "5+8*4^2";

            var matches = Regex.Matches(input, power)
                .Select(
                match => match
                )
                .ToList();

            foreach (Match? match in matches)
            {
                WriteLine(match);
            }

        }

        internal static void Run2()
        {
            string data = "XMASAMXMASMM";
            string pattern = @"XMAS";

            var matches = Regex.Matches(data, pattern)
                .Select(Tokenize)
                .ToList();

            foreach (Token? match in matches)
            {
                WriteLine($"{match}");
            }
        }

        internal static void Run()
        {
            string data = @"why()select()when()$^mul(126,507):^mul(59,335)*}";
            string pattern = @"(?<multiply>mul)\((?<left>\d+),(?<right>\d+)\)|(?<when>when)\(\)|(?<select>select)\(\)|(?<why>why)\(\)|(?<dont>don't)\(\)|(?<do>do)\(\)";

            var matches = Regex.Matches(data, pattern)
                .Select(Tokenize)
                .ToList();

            foreach (Token? match in matches)
            {
                WriteLine($"{match}");
            }
        }

        internal static Token Tokenize(Match match) => match switch
        {
            var whyVar when match.Groups["why"].Success => new Why(whyVar.Value),
            var whenVar when match.Groups["when"].Success => new Why(whenVar.Value),
            var selectVar when match.Groups["select"].Success => new Why(selectVar.Value),
            _ when match.Groups["multiply"].Success => new Multiply(int.Parse(match.Groups["left"].Value, CultureInfo.InvariantCulture.NumberFormat), int.Parse(match.Groups["right"].Value, CultureInfo.InvariantCulture.NumberFormat)),
            _ => new Anything(match.Value),
        };
    }
    internal abstract record Token;

    /// <summary>
    /// error CA1852: Type '****' can be sealed because it has no subtypes in its containing assembly 
    /// and is not externally visible
    /// (https://learn.microsoft.com/dotnet/fundamentals/code-analysis/quality-rules/ca1852)
    /// </summary>
    /// <param name="Value"></param>
    internal sealed record Anything(string Value) : Token;
    internal sealed record Why(string Value) : Token;
    internal sealed record Multiply(int Left, int Right) : Token;
#pragma warning disable CA1812
    internal sealed record When(string Value) : Token;
    internal sealed record Select(string Value) : Token;
#pragma warning restore CA1812
}
