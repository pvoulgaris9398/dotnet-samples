namespace Advent2024
{
    /// <summary>
    /// WIP: Saturday, 4/5/25
    /// </summary>
    internal static class Day02
    {
        internal static void Run()
        {
            /*  Many reports
             *  One report per line
             *  Each report is a list of numbers called "levels"
             *  Levels must be either "gradually increasing" or
             *      "gradually decreasing"
             *  Report is "safe" if both are true:
             *      Levels are either "all increasing" or "all decreasing"
             *      Any two adjacent levels differ by _at least one_ AND
             *      _at most three_
             *
             */

            List<List<int>> allLists = LoadFileData("..\\..\\..\\..\\..\\day2.txt");

            int safeCount = allLists.Count(IsSafe);
            int tolerantSafeCount = allLists.Count(list => list.Expand().Any(IsSafe));

            WriteLine($"        Total count: {allLists.Count}");
            WriteLine($"         Safe count: {safeCount}");
            WriteLine($"Tolerant safe count: {tolerantSafeCount}");
        }

        internal static void Test3(List<int> list)
        {
            WriteLine(new string('*', 80));
            var pairs = list.ToPairs();
            var pairs2 = list.ToPairs_ORIGINAL();
            WriteLine(pairs2);
            foreach (var (prev, next) in pairs)
            {
                WriteLine($"Previous: {prev}\tNext: {next}");
            }
        }

        internal static void Test2()
        {
            var testData = LoadTestData();

            foreach (var item in testData)
            {
                WriteLine(new string('*', 80));
                foreach (var entry in item)
                {
                    Write($"{entry},");
                }
                WriteLine();
                WriteLine(new string('*', 80));
                var triplets = item.ToTriplets();
                foreach (var triplet in triplets)
                {
                    WriteLine(triplet);
                }
            }
        }

        internal static void Test1()
        {
            var testData = LoadTestData();

            var results = testData.ToList();

            int safeCount = results.Count(IsSafe);
            int tolerantSafeCount = results.Count(list => list.Expand().Any(IsSafe));

            var expanded = results[0].Expand().ToList();

            WriteLine($"        Total count: {results.Count}");
            WriteLine($"         Safe count: {safeCount}");
            WriteLine($"Tolerant safe count: {tolerantSafeCount}");
        }

        private static IEnumerable<List<int>> Expand(this List<int> values) =>
            new[] { values }.Concat(Enumerable.Range(0, values.Count).Select(values.ExceptAt));

        private static List<int> ExceptAt(this List<int> values, int index) =>
            [.. values.Take(index), .. values.Skip(index + 1)];

        private static bool IsSafe(this List<int> values) =>
            values.Count < 2 || values.IsSafe(Math.Sign(values[1] - values[0]));

        private static bool IsSafe(this List<int> values, int direction) =>
            values.ToPairs().All(pair => pair.IsSafe(direction));

        private static bool IsSafe(this (int previous, int current) pair, int direction) =>
            Math.Abs(pair.current - pair.previous) >= 1
            && Math.Abs(pair.current - pair.previous) <= 3
            && Math.Sign(pair.current - pair.previous) == direction;

        /// <summary>
        /// The number of triplets in a dataset is equal to:
        /// [Number of Elements] minus (2)
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private static IEnumerable<(int prev, int current, int next)> ToTriplets(
            this List<int> values
        )
        {
            using var enumerator = values.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                yield break; // No _pairs_
            }

            int prev = enumerator.Current;

            if (!enumerator.MoveNext())
            {
                yield break; // No _triplets_
            }

            int current = enumerator.Current;

            while (enumerator.MoveNext())
            {
                yield return (prev, current, enumerator.Current);
                prev = current;
                current = enumerator.Current;
            }
        }

        /// <summary>
        /// The number of pairs in a dataset is equal to:
        /// [Number of Elements] minus (1)
        /// As an example, the number of returns in a set of prices,
        /// where a return is defined as the change in price over a specified
        /// time period as a percentage
        /// Intuitively, you can't have a return value when you only have
        /// one (1) item in the dataset. And you can only have one return
        /// value in a dataset containing two (2) elements.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private static IEnumerable<(int prev, int next)> ToPairs_ORIGINAL(this List<int> values)
        {
            using var enumerator = values.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                yield break; // No _pairs_
            }

            int prev = enumerator.Current;

            while (enumerator.MoveNext())
            {
                yield return (prev, enumerator.Current);
                prev = enumerator.Current;
            }
        }

        private static List<List<int>> LoadFileData(string path) =>
            [.. File.OpenText(path).ReadLines().Select(CommonFuncs.ParseIntsNoSign)];

        private static IEnumerable<List<int>> LoadTestData()
        {
            yield return [11, 10, 9, 7, 6, 4, 2, 1];
            yield return [1, 2, 7, 8, 9, 11, 14, 14];
            yield return [17, 15, 12, 9, 7, 6, 2, 1];
            yield return [1, 3, 2, 4, 5, 9, 12];
            yield return [13, 11, 10, 8, 6, 4, 4, 1];
            yield return [1, 3, 6, 7, 9, 9, 12];
        }
    }
}
