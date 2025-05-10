namespace Algorithms
{
    internal static class Day13
    {
        private static List<string> RealData => CommonFuncs.LoadFileData("..\\..\\..\\..\\..\\day13.txt");
        public static void Run(bool testing = false)
        {
            WriteLine(new string('*', 80));
            WriteLine($"{nameof(Day13)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            WriteLine(RealData);

        }
    }
}
