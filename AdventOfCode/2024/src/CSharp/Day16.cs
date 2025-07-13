using System.Collections.Immutable;

namespace Advent2024
{
    internal static class Day16
    {
        private static List<string> RealData => CommonFuncs.LoadFileData("..\\..\\..\\..\\..\\data\\day16.txt");

        internal static void Run()
        {
            var path = "..\\..\\..\\..\\..\\data\\day16.txt";
            char[][] maze = File.OpenText(path).ReadMaze();

            var (cheapestPath, paths) = maze.FindCheapestPath(1, 1000);

            var pathLengths = paths.Distinct().Count();

            WriteLine($"{nameof(cheapestPath)}: {cheapestPath}");
            WriteLine($"{nameof(pathLengths)}: {pathLengths}");

        }

        private static ReachedState ToReachedState(this Point point) =>
            new(0, [point]);

        internal sealed record ReachedState(int Cost, ImmutableList<Point> Steps);

        private static ReachedState Add(this ReachedState reached, Point point, int cost) =>
        new(0, reached.Steps.Add(point));

        private static ReachedState MergeWith(this ReachedState reached, ReachedState? other) =>
            other is null ? reached
            : reached.Cost < other.Cost ? reached
            : reached with { Steps = reached.Steps.AddRange(other.Steps) };

        private static ReachedState FindCheapestPath(this char[][] maze, int stepCost, int turnCost)
        {
            var startNode = new State(maze.GetStartPosition(), new Direction(0, 1));

            var reach = new Dictionary<State, ReachedState>() { [startNode] = startNode.Position.ToReachedState() };

            var visited = new HashSet<State>();
            var queue = new PriorityQueue<State, int>([(startNode, 0)]);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (maze.IsEnd(current.Position)) return reach[current];
                if (!visited.Add(current)) continue;

                (int cost, _) = reach[current];

                (State state, int cost)[] neighbors =
                [
                    (current.StepForward(), cost+stepCost),
                    (current.TurnLeft(), cost +turnCost),
                    (current.TurnRight(), cost +turnCost)
                ];

                foreach (var neighbor in neighbors)
                {
                    if (!maze.IsEmpty(neighbor.state.Position)) continue;
                    if (reach.TryGetValue(neighbor.state, out ReachedState? reachedNeighbor)
                        && neighbor.cost > reachedNeighbor.Cost) continue;

                    reach[neighbor.state] = reach[current]
                        .Add(neighbor.state.Position, neighbor.cost)
                        .MergeWith(reachedNeighbor);

                    queue.Enqueue(neighbor.state, neighbor.cost);
                }
            }
            throw new InvalidOperationException("There is no exit!");
        }

        private static State StepForward(this State node) =>
            node with { Position = node.Position.Move(node.Orientation) };

        private static State TurnLeft(this State state) =>
            state with { Orientation = new(-state.Orientation.ColumnStep, state.Orientation.RowStep) };

        private static State TurnRight(this State state) =>
            state with { Orientation = new(state.Orientation.ColumnStep, -state.Orientation.RowStep) };

        private static Point Move(this Point point, Direction direction) =>
            new(point.Row + direction.RowStep, point.Column + direction.ColumnStep);

        private static IEnumerable<Point> FindAll(this char[][] maze, char content) =>
            from row in Enumerable.Range(0, maze.Length)
            from column in Enumerable.Range(0, maze[row].Length)
            where maze[row][column] == content
            select new Point(row, column);

        private static Point GetStartPosition(this char[][] maze) =>
            maze.FindAll('S').First();

        private static char At(this char[][] maze, Point point) =>
            maze[point.Row][point.Column];

        private static bool IsEmpty(this char[][] maze, Point point) =>
            maze.At(point) != '#';

        private static bool IsEnd(this char[][] maze, Point point) =>
            maze.At(point) == 'E';

        internal sealed record State(Point Position, Direction Orientation);
        internal sealed record Direction(int RowStep, int ColumnStep);
        internal sealed record Point(int Row, int Column);

        private static char[][] ReadMaze(this TextReader text) =>
            [.. text.ReadLines().Select(line => line.ToCharArray())];

    }
}
