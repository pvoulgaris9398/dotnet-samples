using System.Collections.Immutable;

namespace Advent2024
{
    internal static class Day20Alternate
    {
        private const string Test1 = "..\\..\\..\\..\\..\\data\\day20-test.txt";

        private const string Prod = "..\\..\\..\\..\\..\\data\\day20.txt";

        public static void Run(bool testing = false)
        {
            WriteLine(new string('*', 80));
            WriteLine($"{nameof(Day20Alternate)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            var data = testing ? Test1 : Prod;

            char[][] maze = File.OpenText(data).ReadMaze();

            var paths = maze.ShortestPath().ToArray();

            int shortestPath = paths.Length - 1;

            WriteLine($"Shortest path: {shortestPath}");

        }

        private static ImmutableList<Point> ShortestPath(this char[][] maze)
        {
            var start = maze.FindStart();
            var end = maze.FindEnd();

            var paths = new Dictionary<Point, ImmutableList<Point>> { [start] = [start] };
            var queue = new Queue<Point>([start]);

            while (queue.TryDequeue(out var current))
            {
                if (current == end) return paths[current];

                foreach (var neighbor in current.GetNeighbors(maze))
                {
                    if (paths.ContainsKey(neighbor)) continue;
                    paths[neighbor] = paths[current].Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }

            throw new InvalidOperationException("No path!");

        }

        private static IEnumerable<Point> GetNeighbors(this Point point, char[][] maze) =>
        new[]
        {
            point with { Row = point.Row - 1 }, point with { Row = point.Row + 1 },
            point with { Column = point.Column - 1 }, point with { Column = point.Column + 1 }
        }.Where(maze.IsEmpty);

        private static bool IsEmpty(this char[][] maze, Point point) =>
            maze.IsInside(point) && maze[point.Row][point.Column] != '#';

        private static bool IsInside(this char[][] maze, Point point) =>
            point.Row >= 0 && point.Row < maze.Length &&
            point.Column >= 0 && point.Column < maze[point.Row].Length;

        private static IEnumerable<Point> Find(this char[][] maze, char target) =>
            from row in Enumerable.Range(0, maze.Length)
            from col in Enumerable.Range(0, maze[0].Length)
            where maze[row][col] == target
            select new Point(row, col);

        private static Point FindStart(this char[][] maze) =>
            maze.Find('S').First();

        private static Point FindEnd(this char[][] maze) =>
            maze.Find('E').First();

        private static char[][] ReadMaze(this TextReader text) =>
        [.. text.ReadLines().Select(line => line.ToCharArray())];

        internal sealed record Point(int Row, int Column);
    }
}
