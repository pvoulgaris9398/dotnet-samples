namespace Algorithms
{
    internal static class Day12
    {
        private static List<string> RealData => CommonFuncs.LoadFileData("..\\..\\..\\..\\..\\day12.txt");

        private static readonly List<string> TestData = [
"AAAA"
,"BBCD"
,"BBCC"
,"EEEC"
            ];

        public static void Run(bool testing = false)
        {
            WriteLine(new string('*', 80));
            WriteLine($"{nameof(Day12)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            var data = testing ? [.. TestData] : RealData;
            char[][] map = data.ReadMap();

            WriteLine($"{nameof(map)}: {map}");

            //var temp2 = (testing ? TestData : InputData).Count(75);

            //var result2 = temp2;

            //WriteLine($"{nameof(result2)}: {result2:#,###}");

        }
    }
}
