namespace Algorithms
{
    internal static class DayEight
    {
        internal static void Run()
        {
            /*
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

            var count1 = CommonFuncs
                .LoadFileData("..\\..\\..\\..\\..\\day8.txt");

            foreach (var item in count1)
            {
                WriteLine(item);
            }

        }
    }
}