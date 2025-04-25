using System.Text.RegularExpressions;

namespace Algorithms
{
    internal static class DayEight
    {
        internal static void Run()
        {
            /*
             * lowercase letter, uppercase letter, or digit
             * 
             * Horizontal, Vertical, Diagonal checks
             * Pattern is same letter (antenna) in row, 
             * with "antinode" double the distance on either side of antenna
             * Need to know distance between them, then check if either end has antinode or is off the grid
             * Count all instances of valid antinodes
             * antinodes can occur at locations of other antennas
             * Can antinodes (of different antennas) overlap? Do they get counted more than once?
             */

            WriteLine(new string('*', 80));
            WriteLine($"{nameof(DayEight)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            //var pattern = @"([0-9]|[A-Z]|[a-z]){2}";
            //var pattern = @"\d{2}";
            var pattern = @"(?<mul>mul)\((?<a>\d+),(?<b>\d+)\)";
            var line = "mul(229,919)do()don't()mul(797,721)";

            var matches = Regex.Matches(line, pattern);

            foreach (var match in matches)
            {
                WriteLine(match);
            }

            var data = CommonFuncs
                .LoadFileData("..\\..\\..\\..\\..\\day8.txt");
            int rows = data.Count;
            int columns = data[0].Length;

            var count1 = data.GetAllStrings(rows, columns)
                .Sum(CountOfAntinodes);

            WriteLine($"Count: {count1}");

            foreach (var item in data)
            {
                WriteLine(item);
            }
        }

        private static int CountOfAntinodes(this string line) => 0;
    }
}