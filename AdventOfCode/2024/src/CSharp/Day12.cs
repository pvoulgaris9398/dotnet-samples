namespace Advent2024
{
    internal static class Day12
    {
        private static List<string> RealData => CommonFuncs.LoadFileData("..\\..\\..\\..\\..\\day12.txt");

        private static readonly List<string> TestData1 = [
"AAAA"
,"BBCD"
,"BBCC"
,"EEEC"
            ];

        private static readonly List<string> TestData2 = [
"OOOOO"
,"OXOXO"
,"OOOOO"
,"OXOXO"
,"OOOOO"
            ];

        private static readonly List<string> TestData3 = [
 "RRRRIICCFF"
,"RRRRIICCCF"
,"VVRRRCCFFF"
,"VVRCCCJFFF"
,"VVVVCJJCFE"
,"VVIVCCJJEE"
,"VVIIICJJEE"
,"MIIIIIJJEE"
,"MIIISIJEEE"
,"MMMISSJEEE"
            ];

        public static void Run(bool testing = false)
        {
            WriteLine(new string('*', 80));
            WriteLine($"{nameof(Day12)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            Test1(testing); //1370258 is the correct answer for Part 1!
            // Part 2 was 805814

            Day12Alternate.Run();

        }

        private static IEnumerable<GardenPlot> ToGardenPlots(this char[][] map) =>
            map.SelectMany((row, rowIndex) =>
                row.Select((column, columnIndex) => new GardenPlot(rowIndex, columnIndex, column)));

        private static IEnumerable<Region> DistinctRegions(this IEnumerable<GardenPlot> plots)
        {
            var groups = plots.GroupBy(p => p.Plant).Select(g => new Region(g.Key, [.. g]));
            return groups;
        }

        internal static void Test1(bool testing = false)
        {
            char[][] map = (testing ? TestData3 : RealData).ReadMap();
            var plots = map.ToGardenPlots();
            var regions = plots.DistinctRegions();

            var totalPrice = map
                .ToGardenPlots()
                .DistinctRegions()
                .SelectMany(region => region.ContiguousRegions())
                .Sum(region => region.Price);

            WriteLine($"Total Price: {totalPrice}");

        }

        private static IEnumerable<Region> ContiguousRegions(this Region all)
        {
            HashSet<GardenPlot> candidates = [.. all.Plots];

            while (candidates.Count > 0)
            {
                HashSet<GardenPlot> currentRegion = [];
                var start = candidates.First();
                currentRegion.Add(start);
                candidates.Remove(start);

                while (currentRegion.HasLinksToAny(candidates))
                {
                    foreach (var candidate in candidates)
                    {
                        if (currentRegion.Any(currentPlot => currentPlot.NextTo(candidate)))
                        {
                            currentRegion.Add(candidate);

                            candidates.Remove(candidate);
                        }
                    }
                }

                yield return new(all.Plant, [.. currentRegion]);
            }
        }

        private static bool HasLinksToAny(this IEnumerable<GardenPlot> left, IEnumerable<GardenPlot> right)
        {
            if (left.Any(l => right.Any(r => r.NextTo(l))) || right.Any(r => left.Any(l => l.NextTo(r))))
            {
                return true;
            }
            return false;
        }

        private static bool NextTo(this GardenPlot currentPlot, GardenPlot nextPlot)
        {
            return (currentPlot.Row == nextPlot.Row && Math.Abs(currentPlot.Column - nextPlot.Column) == 1) ||
                (currentPlot.Column == nextPlot.Column && Math.Abs(currentPlot.Row - nextPlot.Row) == 1);
        }

        private static char? ValueAt(this IEnumerable<GardenPlot> plots, (int row, int column) coordinates) => plots.ToArray().ValueAt(coordinates.row, coordinates.column);

        private static char? ValueAt(this GardenPlot[] plots, int row, int column) => plots.FirstOrDefault(p => p.Row == row && p.Column == column)?.Plant;

        private sealed record GardenPlot(int Row, int Column, char Plant)
        {
            public (int row, int column) Left => (Row, Column - 1);
            public (int row, int column) Right => (Row, Column + 1);
            public (int row, int column) Up => (Row + 1, Column);
            public (int row, int column) Down => (Row - 1, Column);
        }

        private sealed record Region(char Plant, GardenPlot[] Plots)
        {
            public int Area => Plots.Length;

            public int Perimeter => Plots.ToList().Select(p => PlotPerimeter(p, Plots)).Sum();

            public int Price => Area * Perimeter;

            private static int PlotPerimeter(GardenPlot plot, IEnumerable<GardenPlot> plots) =>
                (plots.ValueAt(plot.Left) == null ? 1 : 0)
                + (plots.ValueAt(plot.Right) == null ? 1 : 0)
                + (plots.ValueAt(plot.Up) == null ? 1 : 0)
                + (plots.ValueAt(plot.Down) == null ? 1 : 0);

        }
    }
}
