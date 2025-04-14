namespace Algorithms
{
    internal static class DayFiveInitialAttempt
    {

        internal static void Run(bool test = false)
        {
            var data = CommonFuncs.LoadFileData("..\\..\\..\\..\\..\\day5.txt");

            /*  Each element must have rules
             *      that put every other subsequent page after it or it is the last element
             *  Each element must have rules
             *      that put it after every previous element or be the first element
             */

            var rules = data.ToSortRules();
            var allPages = data.ToPages().ToList();

            var sum = allPages
                        .Where(p =>
                        {
                            var pairs = p.ToArray().GeneratePairs().ToList();
                            return pairs.InOrder([.. rules]);
                        }).Sum(p => p.MiddlePage());

            WriteLine($"Sum: {sum}");

        }

        /// <summary>
        /// 
        /// One Test Case 
        /// 
        /// 75|47
        /// 75|61
        /// 75|53
        /// 75|29
        /// 
        /// 75,47,61,53,29
        /// 
        /// </summary>
        internal static List<string> LoadTestData()
            => [
                "75|47"
                ,"75|61"
                ,"75|53"
                ,"75|29"
                ,"47|61"
                ,"47|53"
                ,"47|29"
                ,"61|53"
                ,"61|29"
                ,"53|29"
                ,""
                ,"75,47,61,53,29"
            ];

        internal static void Test()
        {

            (int a, int b)[] rules = [(75, 47), (75, 61), (75, 53), (75, 29)
                ,(47,61),(47,53), (47,29), (61, 53), (61, 29), (53,29)];

            int[] update = [75, 47, 61, 53, 29];

            /*  Each element must have rules
          *  that put every other subsequent page after it or it is the last element
          *  Each element must have rules
          *  that put it after every previous element or be the first element
          */

            var pairs = GeneratePairs(update).ToList();

            WriteLine(pairs);

            var inOrder = pairs.InOrder([.. rules]);

            WriteLine(inOrder);

        }

        private static int MiddlePage(this List<int> pages) =>
            pages[pages.Count / 2];

        private static IEnumerable<(int a, int b)> GeneratePairs(this int[] update)
        {
            var lastIndex = update.Length - 1;
            foreach (var index in Enumerable.Range(0, update.Length))
            {
                var current = update[index];
                var i = index;
                while (i < lastIndex)
                {
                    i++;
                    yield return new(current, update[i]);

                }
            }
            yield break;
        }

        private static bool InOrder(this List<(int Before, int After)> updates, List<(int Before, int After)> rules) => updates.All(rules.Contains);

    }
}