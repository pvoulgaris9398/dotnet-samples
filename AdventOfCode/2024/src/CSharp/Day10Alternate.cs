namespace Advent2024;

internal static class Day10Alternate
{
    internal static List<string> TestDataSet3()
    {
        return [
"..90..9"
,"...1.98"
,"...2..7"
,"6543456"
,"765.987"
,"876...."
,"987...."
            ];

    }
    internal static List<string> TestDataSet2()
    {
        return [
"89010123"
,"78121874"
,"87430965"
,"96549874"
,"45678903"
,"32019012"
,"01329801"
,"10456732"
            ];

    }
    public static void Run(bool testing = false)
    {
        List<string> data = testing ? TestDataSet2() : CommonFuncs.LoadFileData("..\\..\\..\\..\\..\\day10.txt");
        char[][] map = [.. data.Select(row => row.ToCharArray())];

        var startingPoints = map.GetStartingPoints().ToList();

        int totalScore = startingPoints.Sum(start => map.Walk(start).CountHikingTrails(map));
        int distinctTrailScore = startingPoints.Sum(start => map.Walk(start).CountDistinctHikingTrails(map));

        WriteLine($"   Total hiking trails: {totalScore}");
        WriteLine($"Distinct hiking trails: {distinctTrailScore}");
    }

    private static int CountDistinctHikingTrails(this IEnumerable<((int row, int col) point, int count)> visitedPoints, char[][] map) =>
        visitedPoints.Where(node => map.At(node.point) == '9').Sum(node => node.count);

    private static int CountHikingTrails(this IEnumerable<((int row, int col) point, int count)> visitedPoints, char[][] map) =>
        visitedPoints.Count(node => map.At(node.point) == '9');

    private static IEnumerable<((int row, int col) point, int count)> Walk(this char[][] map, (int row, int col) start)
    {
        var pointToCount = new Dictionary<(int row, int col), int>() { [start] = 1 };
        var queue = new Queue<(int row, int col)>();

        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            yield return (current, pointToCount[current]);

            foreach (var neighbor in map.GetUphillNeighbors(current))
            {
                if (pointToCount.ContainsKey(neighbor))
                {
                    pointToCount[neighbor] += pointToCount[current];
                }
                else
                {
                    pointToCount[neighbor] = pointToCount[current];
                    queue.Enqueue(neighbor);
                }
            }
        }
    }

    private static IEnumerable<(int row, int col)> GetUphillNeighbors(this char[][] map, (int row, int col) point) =>
        new[]
        {
            (point.row - 1, point.col), (point.row + 1, point.col),
            (point.row, point.col - 1), (point.row, point.col + 1)
        }
        .Where(neighbor => map.IsUphill(point, neighbor));

    private static bool IsUphill(this char[][] map, (int row, int col) a, (int row, int col) b) =>
        map.IsOnMap(b) && map.At(a) < '9' && map.At(b) == map.At(a) + 1;

    private static bool IsOnMap(this char[][] map, (int row, int col) point) =>
        point.row >= 0 && point.row < map.Length && point.col >= 0 && point.col < map[point.row].Length;

    private static char At(this char[][] map, (int row, int col) point) =>
        map[point.row][point.col];

    private static IEnumerable<(int row, int col)> GetStartingPoints(this char[][] map) =>
        from row in Enumerable.Range(0, map.Length)
        from col in Enumerable.Range(0, map[row].Length)
        where map[row][col] == '0'
        select (row, col);

}
