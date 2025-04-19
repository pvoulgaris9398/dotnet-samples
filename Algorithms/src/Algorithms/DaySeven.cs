using System.Globalization;

namespace Algorithms
{
    internal static class DaySeven
    {
        /*
         * 
         */
        internal static void Run()
        {

            WriteLine(new string('*', 80));
            WriteLine($"{nameof(DaySeven)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            var test1 = new Equation(190, [10, 19]).CanProduceResult(Addition, Multiplication);
            var test2 = new Equation(3267, [81, 40, 27]).CanProduceResult(Addition, Multiplication);
            var test3 = new Equation(292, [11, 6, 16, 20]).CanProduceResult(Addition, Multiplication);

            var count1 = CommonFuncs
                    .LoadFileData("..\\..\\..\\..\\..\\day7.txt")
                    .ReadEquations()
                    .Where(eq => eq.CanProduceResult(Addition, Multiplication))
                    .Sum(eq => eq.Result);

            var count2 = CommonFuncs
                    .LoadFileData("..\\..\\..\\..\\..\\day7.txt")
                    .ReadEquations()
                    .Where(eq => eq.CanProduceResult(Addition, Multiplication, Concatenation))
                    .Sum(eq => eq.Result);

            WriteLine($"Count: {count1}");
            WriteLine($"Count: {count2}");

        }

        private delegate IEnumerable<long> Operator(Equation equation, IEnumerable<long> a, long b);

        private static IEnumerable<long> Addition(this Equation equation, IEnumerable<long> a, long b) =>
            a.Where(x => equation.Result - x >= b).Select(x => x + b);

        private static IEnumerable<long> Multiplication(this Equation equation, IEnumerable<long> a, long b) =>
            a.Where(x => equation.Result / x >= b).Select(x => x * b);

        private static IEnumerable<long> Concatenation(this Equation equation, IEnumerable<long> a, long b)
        {
            string bString = b.ToString(CultureInfo.InvariantCulture);
            foreach (long x in a)
            {
                string concatenated = x.ToString(CultureInfo.InvariantCulture) + bString;
                if (long.TryParse(concatenated, out long value) && value <= equation.Result)
                {
                    yield return value;
                }
            }
        }

        private static bool CanProduceResult(this Equation equation, params Operator[] operators)
        {
            HashSet<long> produced = [equation.Values[0]];

            foreach (var value in equation.Values[1..])
            {
                var expanded = operators.SelectMany(op => op(equation, produced, value));
                produced = [.. expanded];
            }

            return produced.Contains(equation.Result);
        }

        private static IEnumerable<Equation> ReadEquations(this List<string> lines) =>
            lines
            .Select(CommonFuncs.ParseLongsNoSign)
            .Select(values => new Equation(values[0], values[1..]));

        internal sealed record Equation(long Result, List<long> Values);

    }
}
