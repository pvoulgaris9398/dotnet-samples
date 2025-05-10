using System.Globalization;

namespace Algorithms
{
    internal static class Day05
    {
        internal static void Run()
        {
            var data = CommonFuncs.LoadFileData("..\\..\\..\\..\\..\\day5.txt");

            var rules = data.ToSortRules().ToList();

            IComparer<int> comparer = Comparer<int>.Create((a, b) =>
                rules.Contains((a, b)) ? -1
                : rules.Contains((b, a)) ? 1
                : 0);

            var allPages = data.ToPages().ToList();

            var sum1 = allPages
                .Where(pages => pages.IsCorrectlySorted(comparer))
                .Sum(pages => pages.MiddlePage());

            var sum2 = allPages
                .Where(pages => !pages.IsCorrectlySorted(comparer))
                .Sum(pages => pages.Order(comparer).MiddlePage());

            WriteLine($"Sum: {sum1}");
            WriteLine($"Sum: {sum2}");

        }
        private static int MiddlePage(this List<int> pages) =>
            pages[pages.Count / 2];

        private static int MiddlePage(this IEnumerable<int> pages)
        {
            using var half = pages.GetEnumerator();
            using var full = pages.GetEnumerator();

            // Increment the "full" twice, so when the function exits
            // It returns the halfway ("half") value...
            while (full.MoveNext() && half.MoveNext() && full.MoveNext()) { }

            return half.Current;
        }

        private static bool IsCorrectlySorted(this List<int> pages, IComparer<int> comparer) =>
        pages.SelectMany((prev, index) => pages[(index + 1)..].Select(next => (prev, next)))
            .All(pair => comparer.Compare(pair.prev, pair.next) <= 0);

        internal static IEnumerable<List<int>> ToPages(this List<string> text) =>
            text
                .Where(line => line.Contains(','))
                .Select(CommonFuncs.ParseIntsNoSign);

        internal static IEnumerable<(int before, int after)> ToSortRules(this List<string> text) =>
            text
            .TakeWhile(line => !string.IsNullOrWhiteSpace(line))
            .Select(ToSortRules);

        private static (int before, int after) ToSortRules(this string line)
        {
            var parts = line.Split('|');
            return (int.Parse(parts[0], CultureInfo.InvariantCulture), int.Parse(parts[1], CultureInfo.InvariantCulture));
        }
    }
}
