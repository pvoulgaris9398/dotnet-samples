namespace Algorithms
{
    internal static class Day01
    {
        /// <summary>
        /// Example/solution for advent-of-code-2024
        /// Day One
        /// </summary>
        internal static void Run(bool testing = false)
        {
            (List<int> left, List<int> right) = testing ? LoadTestData() : LoadLists("..\\..\\..\\..\\..\\day1.txt");

            WriteLine(new string('*', 80));

            var totalDistance = left.Order()
                /*  Applies a specified function to the corresponding elements of
                 *  two sequences, producing a sequence of results
                 */
                .Zip(right.Order(), (x, y) =>
                {
                    WriteLine($"Left: {x}\tRight: {y}");
                    return Math.Abs(x - y);
                }
                )
                .Sum();

            WriteLine(new string('*', 80));

            /*
             * For every element in the left
             * find all corresponding elements on the right (if any)
             * and sum their values
             * or count the number of occurrences and multiply the count
             * by the value in the left-hand-side
             * (same thing), in effect
             */
            int similarityScore = left
                /*  Correlates the elements of two sequences based on matching keys.
                 *  The default equality comparer is used to compare keys.
                 */
                .Join(right, l => l, r => r, (l, r) =>
                {
                    WriteLine($"Left: {l}\tRight: {r}");
                    return r;
                })
                .Sum();

            WriteLine($"Total Items: {left.Count}");
            WriteLine($"Total Distance: {totalDistance}");
            WriteLine($"Similarity Scores: {similarityScore}");
        }

        private static (List<int> a, List<int> b) LoadLists(string fileName) =>
            CommonFuncs.LoadFileData(fileName).Select(CommonFuncs.ParseIntsNoSign).Transpose().ToPair();

        private static (List<int> left, List<int> right) LoadTestData() =>
            ([2, 2, 3, 3, 3, 4, 7, 9], [2, 3, 3, 3, 3]);

    }
}
