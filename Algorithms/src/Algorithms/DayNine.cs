namespace Algorithms
{
    internal static class DayNine
    {
        private static string TestData1 => "12345";
        private static string TestData2 => "2333133121414131402";

        private static string RealData => File.OpenText("..\\..\\..\\..\\..\\day9.txt").ReadToEnd();

        public static void Test0()
        {
            /*  File ID's: zero based 0...n
             *  First: indicates number of blocks
             *  Second: indicates count of free space
             *  Odd
             *  
             *  Given file ID starting at zero (0) ... n:
             *  Starting at first digit and every other character indicates count of file ID (n) to output
             *  Starting at second digit and every other character indicates count of "." to output
             *  
             */

            var input = TestData1;

            /*  Given "12345"
             *  File ID zero (0) output one (1) time
             *  Followed by 2 blocks of free space followed by...
             *  File ID one (1) output three (3) times
             *  Followed by four (4) blocks of free space followed by...
             *  File ID two (2) output five (5) times....
             *  Done.
             */
            var expectedOutput = "0..111....22222";

            WriteLine($"{nameof(input)}: {input}");
            WriteLine($"{nameof(expectedOutput)}: {expectedOutput}");
        }

        public static void Run(bool testing = false)
        {


            WriteLine(new string('*', 80));
            WriteLine($"{nameof(DayNine)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            var data = testing ? TestData1 : RealData;

            WriteLine($"Data: {data}");
        }
    }
}