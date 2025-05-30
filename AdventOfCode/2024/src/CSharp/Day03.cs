using System.Globalization;
using System.Text.RegularExpressions;
namespace Advent2024
{
    /// <summary>
    /// Sunday, 4/6/25 WIP
    /// Advent of Code: Day3
    /// </summary>
    internal static class Day03
    {
        internal static class Attempt2
        {
            internal static void Run()
            {
                var data = LoadFileData("..\\..\\..\\..\\..\\day3.txt");

                var pairs = data.ParseV3().ToList();

                foreach (var (left, right) in pairs)
                {
                    WriteLine($"({left},{right})");
                }

                var total = pairs.Sum(pair => pair.left * pair.right);

                WriteLine($"Total:{total}");
            }
        }

        internal static class Attempt1
        {
            internal static void Run()
            {
                var data = LoadFileData("..\\..\\..\\..\\..\\failing-test-case.txt");

                var pairs = data
                    .ToPairs()
                    .Where(IsValid);

                var digits = pairs
                    .Parse()
                    .ToList();

                foreach (var (left, right) in digits)
                {
                    WriteLine($"({left},{right})");
                }

                var total = digits.Sum(pair => pair.left * pair.right);

                WriteLine($"Total:{total}");

                foreach (var (prev, next) in pairs)
                {
                    WriteLine($"Previous: {prev}\tNext: {next}");
                }
            }
        }

        internal static void Run()
        {
            /* Valid Sequences (where prev and next are):
            * "(" => Digit
            * Digit => Digit
            *  Digit => Decimal
            *  Decimal => Digit             
            * Digit => Comma
            * Comma => Digit
            * Digit => Digit
            *  Digit => Decimal
            *  Decimal => Digit 
            * Digit => ")"
            * 
            */

            /*  Valid multiplication sequences (or patterns)
             *  are of the form:
             *      open parenthesis
             *      number
             *      comma
             *      close parenthesis
             *  From my reading any spaces or other characters
             *  make these patterns invalid
             */
            var data = LoadFileData("..\\..\\..\\..\\..\\day3.txt");

            var instructions = ParseV4(data).ToList();

            int sum = instructions.OfType<Multiply>().Evaluate();
            int sumWithExclusions = instructions.Evaluate();

            WriteLine($"                Sum: {sum}");
            WriteLine($"Sum with exclusions: {sumWithExclusions}");


            WriteLine(new string('*', 80));
            WriteLine(nameof(Run));
        }

        internal abstract record Instruction;

        internal sealed record Multiply(int Left, int Right) : Instruction
        {
            public int Product => Left * Right;
        }

        internal sealed record Stop : Instruction;

        internal sealed record Continue : Instruction;

        private static int Evaluate(this IEnumerable<Instruction> instructions)
        {
            return instructions.Aggregate(
        (
            /*  Seeding initial value
             */
            sum: 0
            /*  Passing "state" to subsequent iterations
             *  Indicating whether to include this multiplication
             *  in the running sum or not
             */
            , include: true),
        (acc, instruction) => instruction switch
        {
            Continue => (acc.sum, true),// Resets to allow subsequent multiplications to be included
            Stop => (acc.sum, false),   // Causes summing to stop for subsequent multiplications
            /*  Only include when the "include" flag is set to true
             */
            Multiply multiply when acc.include => (acc.sum + multiply.Product, acc.include),
            /* Otherwise, don't include...as default case, just return the current sum
             */
            _ => acc
        }).sum;
        }

        private static IEnumerable<Instruction> ParseV4(this string data)
        {
            return Regex.Matches(data, @"(?<mul>mul)\((?<a>\d+),(?<b>\d+)\)|(?<dont>don't)\(\)|(?<do>do)\(\)")
                    .Select(match =>
                        match.Groups["dont"].Success ? new Stop() as Instruction
                        : match.Groups["do"].Success ? new Continue()
                        : new Multiply(int.Parse(match.Groups["a"].Value, CultureInfo.InvariantCulture.NumberFormat), int.Parse(match.Groups["b"].Value, CultureInfo.InvariantCulture.NumberFormat)));
        }

        private static IEnumerable<(long left, long right)> ParseV3(this string data)
        {
            // Looking for "mul(d+,d+)"

            // This worked but didn't pull enough values
            // var pattern = @"\bmul\(\d+\,\d+\)\b";
            // Looked at Zoran's example and used this and it worked!
            //var pattern = @"(?<mul>mul)\((?<a>\d+),(?<b>\d+)\)";
            var pattern = @"mul\(\d+,\d+\)";

            var result = Regex.Matches(data, pattern)
                .Select(match => match.ToString())
                .Select(s =>
                {
                    var input = s.Replace("mul(", "", StringComparison.InvariantCulture).Replace(")", "", StringComparison.InvariantCulture);
                    return input.Split(',') is string[] elements &&
                    elements.Length == 2
                        ? (long.Parse(elements[0], CultureInfo.InvariantCulture.NumberFormat), long.Parse(elements[1], CultureInfo.InvariantCulture.NumberFormat))
                        : (0L, 0L);
                });

            return result;
        }

        /// <summary>
        /// This does not work correctly either...
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
#pragma warning disable IDE0051 // Remove unused private members
        private static IEnumerable<(long left, long right)> ParseV2(this char[] data)
#pragma warning restore IDE0051 // Remove unused private members
        {
            char[] validCharacters = ['(', ')', ',', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
            var valid = data.Where(c => validCharacters.Contains(c));

            char previousToken = '\0';
            bool lookingForLeft = true;
            bool lookingForRight = false;
            List<char> left = [];
            List<char> right = [];

            foreach (var c in valid)
            {
                if (previousToken == '\0')
                {
                    previousToken = c;
                    continue;
                }
                if (previousToken == '(' && char.IsNumber(c))
                {
                    previousToken = c;
                    lookingForLeft = true;
                    lookingForRight = false;
                    left.Add(c);
                    continue;
                }

                if (previousToken == ',' && char.IsNumber(c))
                {
                    previousToken = c;
                    lookingForLeft = false;
                    lookingForRight = true;
                    right.Add(c);
                    continue;
                }

                if (previousToken != '\0' && char.IsNumber(previousToken) && char.IsNumber(c))
                {
                    previousToken = c;
                    if (lookingForLeft)
                    {
                        left.Add(c);
                        continue;
                    }
                    if (lookingForRight)
                    {
                        right.Add(c);
                        continue;
                    }
                }

                if (previousToken != '\0' && char.IsNumber(previousToken) && c == ')')
                {
                    previousToken = '\0';
                    lookingForLeft = true;
                    lookingForRight = false;

                    yield return (int.Parse(string.Join("", left), CultureInfo.InvariantCulture.NumberFormat), int.Parse(string.Join("", right), CultureInfo.InvariantCulture.NumberFormat));

                }
                previousToken = c;
            }
        }

        private static IEnumerable<(long left, long right)> Parse(this IEnumerable<(char prev, char next)> pairs)
        {
            string left = string.Empty;
            string right = string.Empty;
            var fetchingLeft = true;
            var fetchingRight = false;
            foreach (var (prev, next) in pairs)
            {
                if (next == ',')
                {
                    fetchingLeft = false;
                    fetchingRight = true;
                    continue;
                }
                if (next == ')')
                {
                    fetchingLeft = true;
                    fetchingRight = false;
                    yield return (long.Parse(left, CultureInfo.InvariantCulture.NumberFormat), long.Parse(right, CultureInfo.InvariantCulture.NumberFormat));
                    left = string.Empty;
                    right = string.Empty;
                    continue;
                }

                if (fetchingLeft)
                {
                    left += next;
                }

                if (fetchingRight)
                {
                    right += next;
                }
            }
        }

#pragma warning disable IDE0072 // Add missing cases
        /// <summary>
        /// So, this approach won't work.
        /// I need context from check-to-check, because there could be another
        /// _otherwise_ valid character, but in the wrong position
        /// in other words, this would be wrong:
        /// "(,2"..."(,5") but not be caught by this logic
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        public static bool IsValid(this (char previous, char current) pair) => pair switch
        {
            var (previous, current) when previous == '(' && char.IsDigit(current) => true,
            var (previous, current) when char.IsDigit(previous) && current == ',' => true,
            var (previous, current) when previous == ',' && char.IsDigit(current) => true,
            var (previous, current) when char.IsDigit(previous) && char.IsDigit(current) => true,
            var (previous, current) when char.IsDigit(previous) && current == ')' => true,
            var (_, _) => false
        };
#pragma warning restore IDE0072 // Add missing cases

        private static string LoadFileData(string path) => string.Join("", [.. File.OpenText(path).ReadLines()]);
    }
}
