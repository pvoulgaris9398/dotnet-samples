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

            var r1 = maze.HorizontalCheats().Distinct()
                .Where(i => i.Start.Row == 1 && i.Start.Column == 8
                || i.Finish.Row == 1 && i.Finish.Column == 8)
                .ToList();

            var stepCount = maze.Walk().Count();

            WriteLine($"{nameof(stepCount)}: {stepCount}");

            var cheats = maze.Cheats().ToList();


        }



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

        private static IEnumerable<(Point Start, Point Finish)> VerticalCheats(this char[][] maze) => [];

        private static IEnumerable<(Point Start, Point Finish)> HorizontalCheats(this char[][] maze) =>
        Enumerable.Range(0, maze.Length - 1).SelectMany(i => maze.SelectMany(c => c.HorizontalCheats(i)));

        private static IEnumerable<(Point Start, Point Finish)> HorizontalCheats(this char[] row, int rowIndex)
        {
            if (row.Length < 3) yield break;
            for (int i = 0; i < row.Length - 3; i++)
            {
                char prev = row[i];
                char middle = row[i + 1];
                char next = row[i + 2];

                if (prev == '.' && middle == '#' && next == '.')
                {
                    yield return (new Point(rowIndex, i, prev), new Point(rowIndex, i + 2, next));
                }
            }
            yield break;
        }

        private static IEnumerable<(Point Start, Point Finish)> ToTest(this List<Point> values)
        {
            using var enumerator = values.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                yield break; // No _pairs_
            }

            Point prev = enumerator.Current;

            if (!enumerator.MoveNext())
            {
                yield break; // No _triplets_
            }

            Point middle = enumerator.Current;

            while (enumerator.MoveNext())
            {
                var next = enumerator.Current;
                if (prev.Value == '.' && middle.Value == '#' && next.Value == '.')
                {
                    yield return (prev, next);
                }

                prev = middle;
                middle = next;
            }
            yield break;
        }

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
