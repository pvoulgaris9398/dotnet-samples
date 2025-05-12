namespace Algorithms
{
    internal static class Day13
    {
        private static List<string> RealData => CommonFuncs.LoadFileData("..\\..\\..\\..\\..\\day13.txt");

        private static readonly List<string> TestData1 = [
"Button A: X+94, Y+34"
,"Button B: X+22, Y+67"
,"Prize: X=8400, Y=5400"
];
        public static void Run(bool testing = false)
        {
            WriteLine(new string('*', 80));
            WriteLine($"{nameof(Day13)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            WriteLine(RealData);

        }
    }
}
