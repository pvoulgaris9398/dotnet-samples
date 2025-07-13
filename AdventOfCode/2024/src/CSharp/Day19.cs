namespace Advent2024
{
    internal static class Day19
    {

        private const string Prod = "..\\..\\..\\..\\..\\data\\day19.txt";

        private const string Test1 = "..\\..\\..\\..\\..\\data\\day19-test.txt";

        internal static (IEnumerable<string>, IEnumerable<string>) Data(bool testing = false)
        {
            TextReader file = File.OpenText(testing ? Test1 : Prod);
            var towels = file.ReadTowels();
            var pattterns = file.ReadPatterns();
            return (towels.ToList(), pattterns.ToList());
        }

        public static void Run((IEnumerable<string>, IEnumerable<string>) data)
        {
            WriteLine(new string('*', 80));
            WriteLine($"{nameof(Day19)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            var (towels, patterns) = data;

            var pathCount = patterns.Count(p => p.Paths(towels) > 0);
            var totalCount = patterns.Sum(p => p.Paths(towels));

            WriteLine($"{nameof(pathCount)}: {pathCount}");
            WriteLine($"{nameof(totalCount)}: {totalCount}");
        }

        internal static void Benchmark()
        {
            string[] towels = ["r", "wr", "b", "g", "bwu", "rb", "gb", "br"];

            //WriteLine("bbrgwb".Paths(towels));

            WriteLine("brwrr".Paths(towels));

            WriteLine("ubwu".Paths(towels));

        }

        internal static long Paths(this string pattern, IEnumerable<string> towels)
        {
            long[] counts = new long[pattern.Length + 1];
            counts[0] = 1;

            for (int i = 0; i < pattern.Length; i++)
            {
                if (counts[i] == 0) continue;

                //string slice = pattern.AsSpan().Slice(i, pattern.Length - i).ToString();
                string slice = pattern.Substring(i);
                foreach (string towel in towels.Where(slice.StartsWith))
                {
                    counts[i + towel.Length] += counts[i];
                }
            }

            return counts[pattern.Length];

        }

        private static IEnumerable<string> ReadTowels(this TextReader text) =>
            text
            .ReadLine()
            ?.Split(", ") ?? Enumerable.Empty<string>();

        private static IEnumerable<string> ReadPatterns(this TextReader text) =>
            text
            .ReadLines()
            .Where(l => !string.IsNullOrWhiteSpace(l));
    }
}
