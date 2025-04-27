namespace Algorithms
{
    internal static class DayEight
    {
        private static string[] TestData => [
"............",
"........0...",
".....0......",
".......0....",
"....0.......",
"......A.....",
"............",
"............",
"........A...",
".........A..",
"............",
"............"
            ];

        private static List<string> RealData => CommonFuncs.LoadFileData("..\\..\\..\\..\\..\\day8.txt");
        internal static void Test1()
        {
            foreach (var antenna in TestData.ToList().Antennas()) { WriteLine(antenna); }
        }

        internal static void Test2()
        {
            var line = new Line(new Point(1, 28), new Point(1, 35));

            foreach (var antinode in line.AntinodesFor([.. TestData])) { WriteLine(antinode); }

        }

        internal static void Test3()
        {
            foreach (var point in RealData.ToList().Antennas().Lines().Antinodes(RealData)) { WriteLine(point); }
        }

        internal static void Run(bool testing = false)
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

            var data = testing ? [.. TestData] : RealData;

            int rows = data.Count;
            int columns = data[0].Length;

            var count1 = data.ToList().Antennas().Lines().Antinodes(data).Count();

            WriteLine($"Count: {count1}");

        }

        internal static IEnumerable<Line> Lines(this IEnumerable<Antenna> antennas)
        {
            var frequencies = antennas.Select(a => a.Frequency).Distinct().ToList();

            foreach (var f in frequencies)
            {
                var temp = antennas.Where(a => a.Frequency == f)
                    .Select(a => a.Point)
                    .ToList();

                var temp2 = temp.SelectMany((prev, index) => temp[(index + 1)..].Select(next => new Line(prev, next))).ToList();

                foreach (var element in temp2)
                {
                    yield return element;
                }

            }

            yield break;
        }

        internal static IEnumerable<Antenna> Antennas(this List<string> data)
        {
            return data.SelectMany((row, rowIndex) =>
                Enumerable.Range(0, row.Length)
                    .Where(columnIndex => Frequencies.Contains(row[columnIndex]))
                    .Select(columnIndex => new Antenna(new Point(rowIndex, columnIndex), row[columnIndex])));
        }

        internal sealed record Antenna(Point Point, char Frequency);

        internal static IEnumerable<Point> Antinodes(this IEnumerable<Line> lines, List<string> data)
            => lines.SelectMany(line => line.AntinodesFor(data));

        internal static IEnumerable<Point> AntinodesFor(this Line line, List<string> data)
        {
            var deltaX = Math.Abs(line.First.X - line.Second.X);
            var deltaY = Math.Abs(line.First.Y - line.Second.Y);

            var antinode1 = new Point(line.First.X - deltaX, line.First.Y - deltaY);
            var antinode2 = new Point(line.Second.X + deltaX, line.Second.Y + deltaY);

            if (antinode1.OnGrid(data) && antinode2.OnGrid(data))
            {
                yield return antinode1;
                yield return antinode2;
            }

            yield break;

        }

        internal static bool OnGrid(this Point point, List<string> data) => point.X >= 0 && point.X < data[0].Length && point.X <= data.Count && point.Y <= data[point.X].Length;

        internal sealed record Line(Point First, Point Second)
        {
            internal double Distance()
            {
                var deltaX = Math.Abs(First.X - Second.X);
                var deltaY = Math.Abs(First.Y - Second.Y);
                return Math.Sqrt(deltaX - deltaY);
            }

        }

        internal sealed record Point(int X, int Y);

        /// <summary>
        /// TODO: Come up with a better way, of course.
        /// </summary>
        internal static char[] Frequencies = [
            'a', 'b', 'c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'
            ,'A','B', 'C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
            ,'1','2','3','4','5','6','7','8','9','0'
            ];
    }
}