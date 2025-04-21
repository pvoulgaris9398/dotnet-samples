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

            //var test2 = new Equation(190, [10, 19]).CanProduceResult(Addition, Multiplication);
            //var test3 = new Equation(3267, [81, 40, 27]).CanProduceResult(Addition, Multiplication);
            //var test4 = new Equation(292, [11, 6, 16, 20]).CanProduceResult(Addition, Multiplication);
            //var test5 = new Equation(9693533, [560, 33, 54, 523, 251]).CanProduceResult(Addition, Multiplication);
            //var test6 = new Equation(1691420808, [33, 720, 4, 76, 84, 66]).CanProduceResult(Addition, Multiplication);
            //var test7 = new Equation(27537, [5, 2, 2, 4, 296, 73, 89]).CanProduceResult(Addition, Multiplication);
            //var test8 = new Equation(1258023882, [4, 79, 534, 502, 61, 3, 3, 9]).CanProduceResult(Addition, Multiplication);
            //var test9 = new Equation(2430252, [58, 1, 3, 9, 47, 71, 5, 19, 418]).CanProduceResult(Addition, Multiplication);

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
            a.Where(x =>
            //equation.Result - x >= b
            {
                var temp = equation.Result - x;
                if (temp >= b)
                {
                    return true;
                }
                else
                {
                    //WriteLine($"[{nameof(Addition)}]: Current: {equation.Result}\tx: {x}\tb:{b}");
                    return false;
                }
            }).Select(x => x + b);

        private static IEnumerable<long> Multiplication(this Equation equation, IEnumerable<long> a, long b) =>
            a.Where(x =>
            {
                var temp = equation.Result / x;
                if (temp >= b)
                {
                    return true;
                }
                else
                {
                    //WriteLine($"[{nameof(Multiplication)}]: Current: {equation.Result}\tx: {x}\tb:{b}");
                    return false;
                }
            }).Select(x => x * b);

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
                var expanded = operators.SelectMany(op => op(equation, produced, value)).ToList();
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
