namespace Advent2024
{
    internal static class Day10Research
    {
        internal static List<string> TestDataSet3()
        {
            return ["..90..9", "...1.98", "...2..7", "6543456", "765.987", "876....", "987...."];
        }

        internal static List<string> TestDataSet2()
        {
            return
            [
                "89010123",
                "78121874",
                "87430965",
                "96549874",
                "45678903",
                "32019012",
                "01329801",
                "10456732",
            ];
        }

        public static void Run()
        {
            var data = TestDataSet3();
            //var data = CommonFuncs.LoadFileData("..\\..\\..\\..\\..\\day10.txt");
            char[][] map = [.. data.Select(row => row.ToCharArray())];

            int totalScore = map.GetTrailHeads().Sum(trailhead => trailhead.GetScore(map));
            int totalRatings = map.GetTrailHeads().Sum(trailhead => trailhead.GetRating(map));

            var temp = map.GetTrailHeads()
                .SelectMany(trailhead => trailhead.WalkFrom(map))
                .ToList();

            var temp2 = temp.Where(state => map.At(state.point) == '9').ToList();

            WriteLine($"{nameof(totalScore)}: {totalScore}");
            WriteLine($"{nameof(totalRatings)}: {totalRatings}");
        }

        private static IEnumerable<((int row, int column) point, int count)> WalkFrom(
            this (int row, int column) trailhead,
            char[][] map
        )
        {
            var pathsCount = new Dictionary<(int row, int column), int>() { [trailhead] = 1 };
            var queue = new Queue<(int row, int column)>(); // breadth-first search

            queue.Enqueue(trailhead);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                yield return (current, pathsCount[current]);

                foreach (var neighbor in map.GetUphillNeighbors(current))
                {
                    if (pathsCount.ContainsKey(neighbor))
                    {
                        pathsCount[neighbor] += pathsCount[current];
                    }
                    else
                    {
                        pathsCount[neighbor] = pathsCount[current];
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        private static int GetScore(this (int row, int column) trailhead, char[][] map) =>
            trailhead.WalkFrom(map).Count(state => map.At(state.point) == '9');

        private static int GetRating(this (int row, int column) trailhead, char[][] map) =>
            trailhead
                .WalkFrom(map)
                .Where(state => map.At(state.point) == '9')
                .Sum(state => state.count);

        private static IEnumerable<(int row, int column)> GetUphillNeighbors(
            this char[][] map,
            (int row, int column) point
        ) =>
            new[]
            {
                (point.row - 1, point.column),
                (point.row + 1, point.column),
                (point.row, point.column - 1),
                (point.row, point.column + 1),
            }.Where(neighbor => map.IsUphill(point, neighbor));

        private static bool IsUphill(
            this char[][] map,
            (int row, int column) a,
            (int row, int column) b
        ) => map.IsOnMap(a) && map.IsOnMap(b) && map.At(b) == map.At(a) + 1; //<== nice.

        private static bool IsOnMap(this char[][] map, (int row, int column) point) =>
            point.row >= 0
            && point.row < map.Length
            && point.column >= 0
            && point.column < map[point.row].Length;

        private static char At(this char[][] map, (int row, int column) point) =>
            map[point.row][point.column];

        private static IEnumerable<(int row, int column)> GetTrailHeads(this char[][] map) =>
            from row in Enumerable.Range(0, map.Length)
            from column in Enumerable.Range(0, map[row].Length)
            where map[row][column] == '0'
            select (row, column);
    }
}
