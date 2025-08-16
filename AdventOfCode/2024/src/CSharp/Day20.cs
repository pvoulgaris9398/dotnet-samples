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

            var stepCount = maze.Walk().Count();

            WriteLine($"{nameof(stepCount)}: {stepCount}");
        }

        private static char[][] ReadMaze(this TextReader text) =>
            [.. text.ReadLines().Select(line => line.ToCharArray())];

        private static Point GetStartPosition(this char[][] maze) => maze.FindAll('S').First();

        private static Point GetEndPosition(this char[][] maze) => maze.FindAll('E').First();

        private static bool Contains(this char[][] maze, (int Row, int Column) point) =>
            point.Row >= 0 && point.Row < maze.Length &&
            point.Column >= 0 && point.Column < maze[point.Row].Length;

        private static IEnumerable<Point> FindAll(this char[][] maze, char content) =>
            from row in Enumerable.Range(0, maze.Length)
            from column in Enumerable.Range(0, maze[0].Length)
            where maze[row][column] == content
            select new Point(row, column);

        private static HashSet<(int Row, int Column)> Visited = [];

        /*
         * Any sequence of three Points where 1 -> 2 -> 3 where 1 = '.' and 3 = '.' and 2 = '#'
         */
        private static IEnumerable<(Point first, Point second)> PossibleCheats(this char[][] maze) => [];

        private static IEnumerable<(Point first, Point second)> HorizontalCheats(this char[][] maze) =>
            Enumerable.Range(0, maze.Length)
            .SelectMany(rowIndex => maze[rowIndex].HorizontalCheats(rowIndex));


        private static IEnumerable<(Point first, Point second)> VerticalCheats(this char[][] maze) => [];

        /*
         * WIP - Most likely doesn't actually work
         */
        private static IEnumerable<(Point first, Point second)> HorizontalCheats(this char[] line, int rowIndex)
        {
            var enumerator = line.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield break;
            }

            int prev = (int)enumerator.Current;

            if (!enumerator.MoveNext())
            {
                yield break;
            }

            int middle = (int)enumerator.Current;

            while (enumerator.MoveNext())
            {
                int next = (int)enumerator.Current;

                if (line[prev] == '.' && line[middle] == '#' && line[next] == '.')
                {
                    yield return (new Point(rowIndex, prev), new Point(rowIndex, next));
                }
                prev = middle;
                middle = next;
            }

        }

        private static Point Next(this Point point, char[][] maze) =>
            new[]
            {
            (point.Row - 1, point.Column), (point.Row + 1, point.Column),
            (point.Row, point.Column - 1), (point.Row, point.Column + 1)
            }
            .Where(neighbor => (maze.At(neighbor) == '.' || maze.At(neighbor) == 'E') && !Visited.Contains(neighbor))
            .Select(p => new Point(p.Item1, p.Item2))
            .First();

        private static char At(this char[][] map, (int Row, int Column) point) =>
            map[point.Row][point.Column];

        private static IEnumerable<Point> Walk(this char[][] map)
        {
            Point next = map.GetStartPosition();
            Point end = map.GetEndPosition();
            while (true)
            {
                Visited.Add((next.Row, next.Column));
                next = Next(next, map);
                yield return next;
                if (next == end) break;
            }
        }

        internal sealed record Point(int Row, int Column);
    }
}
