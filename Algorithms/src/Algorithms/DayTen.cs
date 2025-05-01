namespace Algorithms
{
    internal static class DayTen
    {
        private static string TestData1 => "12345";
        private static string TestData2 => "2333133121414131402";

        /// <summary>
        /// 0..111....22222
        /// </summary>
        /// 

        internal static List<string> TestDataSet1()
        {
            return [
"...0..."
,"...1..."
,"...2..."
,"6543456"
,"7.....7"
,"8.....8"
,"9.....9"
                ];

        }

        private static List<List<int>> LoadFileData(string path) => [.. File.OpenText(path).ReadLines().Select(CommonFuncs.ParseIntsNoSign)];

        private static List<List<int>> LoadTestData(List<string> data) => [.. data.Select(CommonFuncs.ParseIntsNoSign)];

        public static void Run(bool testing = false)
        {
            WriteLine(new string('*', 80));
            WriteLine($"{nameof(DayTen)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            var map = testing ? LoadTestData(TestDataSet1()) : LoadFileData("..\\..\\..\\..\\..\\day10.txt");

            WriteLine(map);

        }
    }
}