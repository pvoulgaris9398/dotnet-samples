namespace Algorithms
{
    /// <summary>
    /// WIP: Saturday, 4/5/25
    /// </summary>
    internal static class DayTwo
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
        }

        internal static void Test()
        {
            var testData = TestData();

            var results = testData.ToList();

            foreach (var item in results)
            {

                Console.WriteLine(Check(Differences(item)));
            }
        }

        private static bool Check(IEnumerable<int> input)
        {
            int? current = null;
            foreach (var i in input)
            {
                if (current == null)
                {
                    current = Math.Abs(i);
                    continue;
                }
                if (Math.Abs((int)current) + 3 <= Math.Abs(i)) return false;
            }
            return true;
        }

        private static IEnumerable<int> Differences(List<int> input)
        {
            int? current = null;
            foreach (var i in input)
            {
                if (current == null)
                {
                    current = i;
                    continue;
                }
                yield return (int)(i - current);
            }
        }

        private static bool Safe(List<int> input)
        {



            var result = input
                .Skip(1)
                .Zip(input, (current, next) => current > next)
                .ToArray();

            Console.WriteLine(nameof(Safe));
            return true;
        }

        private static IEnumerable<List<int>> TestData()
        {
            yield return new List<int> { 7, 6, 4, 2, 1 };
            yield return new List<int> { 1, 2, 7, 8, 9 };
            yield return new List<int> { 9, 7, 6, 2, 1 };
            yield return new List<int> { 1, 3, 2, 4, 5 };
            yield return new List<int> { 8, 6, 4, 4, 1 };
            yield return new List<int> { 1, 3, 6, 7, 9 };
        }




    }
}
