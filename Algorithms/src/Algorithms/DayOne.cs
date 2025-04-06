namespace Algorithms
{
    internal static class DayOne
    {
        /// <summary>
        /// Example/solution for advent-of-code-2024
        /// Day One
        /// </summary>
        internal static void Run()
        {
            (List<int> left, List<int> right) = LoadLists();

            Console.WriteLine(new string('*', 80));

            var totalDistance = left.Order()
                /*  Applies a specified function to the corresponding elements of
                 *  two sequences, producing a sequence of results
                 */
                .Zip(right.Order(), (x, y) =>
                {
                    Console.WriteLine($"Left: {x}\tRight: {y}");
                    return Math.Abs(x - y);
                }
                )
                .Sum();

            Console.WriteLine(new string('*', 80));

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
                    Console.WriteLine($"Left: {l}\tRight: {r}");
                    return r;
                })
                .Sum();

            Console.WriteLine($"Total Items: {left.Count}");
            Console.WriteLine($"Total Distance: {totalDistance}");
            Console.WriteLine($"Similarity Scores: {similarityScore}");
        }

        private static (List<int> left, List<int> right) LoadLists() =>
            (new List<int> { 2, 2, 3, 3, 3, 4, 7, 9 }, new List<int> { 2, 3, 3, 3, 3 });

    }
}
