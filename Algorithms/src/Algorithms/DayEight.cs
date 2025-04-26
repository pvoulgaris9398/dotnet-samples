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

            var data = CommonFuncs.LoadFileData("..\\..\\..\\..\\..\\day8.txt");

            int rows = data.Count;
            int columns = data[0].Length;

            var count1 = data.GetAllLines()
                .SelectMany(Antinodes)
                .Sum();

            WriteLine($"Count of Antinodes: {count1}");

            var tester = "....O........r....O...e....e.....Wt...............";

            var count2 = tester.Antinodes().Sum();

            WriteLine($"Count2: {count2}");

        }

        private static List<string> GetAllLines(this List<string> data) => data;

        private static IEnumerable<int> FindIndicesOf(this string data, char c)
        {
            int index = 0;
            foreach (var character in data)
            {
                if (character == c)
                {
                    yield return index;
                }

                index++;
            }
            yield break;
        }

        private static IEnumerable<int> Antinodes(this string data)
        {
            var temp = Antennas.Where(data.Contains).Select(vc => vc).ToList();

            foreach (var c in temp)
            {
                if (data.FindIndicesOf(c).ToList() is var result &&
                    result.Count == 2)
                {
                    var distance = Math.Abs(result[0] - result[1]);
                    if (result[0] - distance >= 0 && result[1] + distance <= data.Length)
                    {
                        yield return 2;
                    }
                }
            }

            yield break;
        }

        /// <summary>
        /// TODO: Come up with a better way, of course.
        /// </summary>
        internal static char[] Antennas = [
            'a', 'b', 'c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'
            ,'A','B', 'C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
            ,'1','2','3','4','5','6','7','8','9','0'
            ];
    }
}