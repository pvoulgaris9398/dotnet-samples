namespace Advent2024
{
    internal static class Day19
    {
        private const string Prod = "..\\..\\..\\..\\..\\data\\day19.txt";

        private const string Test1 = "..\\..\\..\\..\\..\\data\\day19-test.txt";
        internal static void Run(bool testing = true)
        {
            WriteLine(new string('*', 80));
            WriteLine($"{nameof(Day07)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            TextReader file = File.OpenText(testing ? Test1 : Prod);

            var towels = file.ReadTowels().ToList();

            var patterns = file.ReadPatterns().ToList();

            WriteLine(towels.Count);

            WriteLine(patterns.Count);
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
