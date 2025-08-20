namespace Advent2024
{
    internal static class Day20
    {
        private const string Test1 = "..\\..\\..\\..\\..\\data\\day20-test.txt";

        private const string Prod = "..\\..\\..\\..\\..\\data\\day20.txt";

        public static void Run(bool testing = false)
        {
            WriteLine(new string('*', 80));
            WriteLine($"{nameof(Day20)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            var data = testing ? Test1 : Prod;

            char[][] maze = File.OpenText(data).ReadMaze();

            var paths = maze.Walk().ToArray();

            var stepCount = paths.Length - 1;

            int cheatingPaths2 = maze.FindSaves(paths, 2).Count(saved => saved >= 100);

            WriteLine($"{nameof(stepCount)}: {stepCount}");
            WriteLine($"Cheating paths: {cheatingPaths2}");

        }

        private static IEnumerable<int> FindSaves(this char[][] maze, Point[] path, int maxCut)
        {
            for (int i = 0; i < path.Length - 1; i++)
            {
                for (int j = i + 1; j < path.Length; j++)
                {
                    int distance = path[i].DistanceTo(path[j]);
                    if (distance > maxCut) continue;
                    if (distance >= j - i) continue;
                    yield return j - i - distance;
                }
            }
        }

        private static int DistanceTo(this Point a, Point b) =>
            Math.Abs(a.Row - b.Row) + Math.Abs(a.Column - b.Column);

        private static char[][] ReadMaze(this TextReader text) =>
            [.. text.ReadLines().Select(line => line.ToCharArray())];

        private static Point GetStartPosition(this char[][] maze) => maze.AllPoints().First(p => p.Value == 'S');

        private static Point GetEndPosition(this char[][] maze) => maze.AllPoints().First(p => p.Value == 'E');

        private static bool Contains(this char[][] maze, (int Row, int Column) point) =>
            point.Row >= 0 && point.Row < maze.Length &&
            point.Column >= 0 && point.Column < maze[point.Row].Length;

        private static IEnumerable<Point> AllPoints(this char[][] maze) =>
            from row in Enumerable.Range(0, maze.Length)
            from column in Enumerable.Range(0, maze[0].Length)
            select new Point(row, column, maze[row][column]);

        private static HashSet<(int Row, int Column)> Visited = [];

        /*
         * Any sequence of three Points where 1 -> 2 -> 3 where 1 = '.' and 3 = '.' and 2 = '#'
         */

        private static IEnumerable<(Point Start, Point Finish)> Cheats(this char[][] maze) =>
            maze.HorizontalCheats().Concat(maze.VerticalCheats());

        private static IEnumerable<(Point Start, Point Finish)> VerticalCheats(this char[][] maze) =>
            Enumerable.Range(0, maze[0].Length)
                .Select(columnIndex => maze.AllPoints().Where(p => p.Column == columnIndex).ToList())
                .Take(3)
                .Where(x => x[0].Value == '.' && x[1].Value == '#' && x[2].Value == '.')
                .Select(x => (x[0], x[2]));

        private static IEnumerable<(Point Start, Point Finish)> HorizontalCheats(this char[][] maze) =>
            Enumerable.Range(0, maze.Length)
                .Select(rowIndex => maze.AllPoints().Where(p => p.Row == rowIndex).ToList())
                .Take(3)
                .Where(x => x[0].Value == '.' && x[1].Value == '#' && x[2].Value == '.')
                .Select(x => (x[0], x[2]));

        private static Point Next(this Point point, char[][] maze) =>
            new[]
            {
            (point.Row - 1, point.Column), (point.Row + 1, point.Column),
            (point.Row, point.Column - 1), (point.Row, point.Column + 1)
            }
            .Where(neighbor => (maze.At(neighbor) == '.' || maze.At(neighbor) == 'E') && !Visited.Contains(neighbor))
            .Select(p => new Point(p.Item1, p.Item2, maze[p.Item1][p.Item2]))
            .First();

        private static char At(this char[][] map, (int Row, int Column) point) =>
            map[point.Row][point.Column];

        private static IEnumerable<Point> Walk(this char[][] map)
        {
            Point next = map.GetStartPosition();
            Point end = map.GetEndPosition();
            yield return next;
            while (true)
            {
                Visited.Add((next.Row, next.Column));
                next = Next(next, map);
                yield return next;
                if (next == end) break;
            }
        }

        internal sealed record Point(int Row, int Column, char Value);
    }
}
