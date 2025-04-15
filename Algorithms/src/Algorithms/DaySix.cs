namespace Algorithms
{
    internal static class DaySix
    {

        internal static bool Testing { get; private set; }

        internal static void Test(bool testing = false)
        {
            Testing = testing;

            var data = testing ? LoadTestData() : CommonFuncs.LoadFileData("..\\..\\..\\..\\..\\day6.txt");

            var grid = data
                .ToLocations();

            var count = grid
                .Moves()
                .Distinct()
                .Count();

            WriteLine($"Count is: {count}");

        }

        internal static IEnumerable<Location> Moves(this List<List<Location>> grid)
        {
            var direction = Direction.Up;
            var location = StartingPoint(grid);
            yield return location;

            if (Testing) { WriteLine($"Starting Location: {location} Facing: {direction}"); }

            while (CanMove(grid, location))
            {
                direction = grid.ChangeDirection(direction, location);
                location = NextMove(grid, direction, location);

                yield return location;

                if (Testing)
                {
                    WriteLine($"Next Location: {location} Facing: {direction}");
                }
            }
        }

        internal static bool CanMove(this List<List<Location>> grid, Location current)
            => current.Row > 0 && current.Row < grid.Count - 1
            && current.Column > 0 && current.Column < grid[0].Count - 1;

        internal static bool IsBlocked(this Location location) => location.Data == '#';

        internal static Location NextMove(this List<List<Location>> grid, Direction direction, Location current) => direction switch
        {
            Direction.Up => grid[current.Row - 1][current.Column],
            Direction.Right => grid[current.Row][current.Column + 1],
            Direction.Down => grid[current.Row + 1][current.Column],
            Direction.Left => grid[current.Row][current.Column - 1],
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };

        internal static Direction ChangeDirection(this List<List<Location>> grid, Direction direction, Location current) =>
            (grid.NextMove(direction, current).IsBlocked(), direction) switch
            {
                (false, _) => direction,
                (true, Direction.Up) => Direction.Right,
                (true, Direction.Right) => Direction.Down,
                (true, Direction.Down) => Direction.Left,
                (true, Direction.Left) => Direction.Up,
                _ => throw new ArgumentOutOfRangeException(nameof(direction))
            };

        internal enum Direction
        {
            Up, Down, Left, Right
        }
        internal sealed record Location(int Row, int Column, char Data);

        internal static List<List<Location>> ToLocations(this List<string> data)
            => [.. data.Select((row, rowIndex) => Enumerable.Range(0, data[0].Length).Select(columnIndex => new Location(rowIndex, columnIndex, row[columnIndex])).ToList())];

        internal static Location StartingPoint(this List<List<Location>> grid)
        {
            foreach (var row in grid)
            {
                foreach (var column in row)
                {
                    if (column.Data == '^')
                    {
                        return column;
                    }
                }
            }
            return new Location(-1, -1, '\0');
        }

        internal static void Notes()
        {
            /* First we have to find his starting location
             * 
             * Then we "watch" him move....While
             * 
             * MOVE TO NEXT LOCATION
             * 
             * If he can move in same direction (up/down/left/right) 
             *      (keep track of direction)
             *      (keep track of current and visited "locations")
             *  ==> MOVE
             *      
             *  If "Can't move", turn 90% Clockwise (to the right) => new direction
             *              * 
             *  ==> MOVE
             *              * 
             * DEFINITION OF: "Can't move"
             *   - Obstacle in front OR at beginning or end of row or beginning of end of column
             *   - If the "move" would put him off the grid, stop/return results
             * 
             */
            WriteLine(new string('*', 80));
            WriteLine(nameof(Notes));
            WriteLine(new string('*', 80));

        }

        internal static List<string> LoadTestData()
        {
            return [
"....#....."
,".........#"
,".........."
,"..#......."
,".......#.."
,".........."
,".#..^....."
,"........#."
,"#........."
,"......#..."
                ];

        }
    }
}