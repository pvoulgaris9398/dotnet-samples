#!/usr/bin/env -S dotnet run

#:project ./AdventOfCode/AdventOfCode.csproj

using AdventOfCode;

if (args.Length > 0)
{
    Action actionToExecute = args[0] switch
    {
        "test0" => Tests.Test0,
        _ => () => Console.WriteLine("Unrecognized command-line argument"),
    };
    actionToExecute();
    return;
}

Console.WriteLine($"Part 1 (Expecting: 1516): {Solution.SolvePart1()}");

Console.WriteLine($"Part 2 (Expecting: 9122): {Solution.SolvePart2()}");

internal static class Solution
{
    private const int Year = 2025;
    private const int Day = 4;
    internal static string Source => $"Advent of Code - {Year}";
    internal static string Name => $"Day {Day}";
    internal static string FileName => $"./AdventOfCode/2025/data/day{Day}.txt";

    /*
    1473 => wrong, too low
    1516 => correct
    */
    internal static int SolvePart1(bool debug = false) =>
        (debug ? Test : FileName.ToLines()).ToMatrix().AccessibleRolls().Count();

    /*
    9122 => correct
    */
    internal static int SolvePart2(bool debug = false) =>
        (debug ? Test : FileName.ToLines()).ToMatrix().Removed();

    internal static int Removed(this char[][] matrix)
    {
        while (matrix.AccessibleRolls().Any())
        {
            foreach (var accessible in matrix.AccessibleRolls().ToList())
            {
                accessible.Remove(matrix);
            }
        }
        return matrix.Cells().Count(c => c.At(matrix) == 'X');
    }

    internal static void Remove(this (int row, int col) cell, char[][] matrix) =>
        matrix[cell.row][cell.col] = 'X';

    internal static IEnumerable<(int, int)> Rolls(this char[][] matrix) =>
        matrix.Cells().Where(c => c.At(matrix) == '@');

    internal static IEnumerable<(int, int)> AccessibleRolls(this char[][] matrix) =>
        matrix.Rolls().Where(r => r.Neighbors(matrix).Blocked(matrix).Count() < 4);

    internal static IEnumerable<(int, int)> Blocked(
        this IEnumerable<(int, int)> cells,
        char[][] matrix
    ) => cells.Where(c => c.At(matrix) == '@');

    internal static IEnumerable<(int, int)> Neighbors(
        this (int row, int col) cell,
        char[][] _ /*matrix*/
    ) =>
        [
            (cell.row + 1, cell.col - 1),
            (cell.row + 1, cell.col),
            (cell.row + 1, cell.col + 1),
            (cell.row, cell.col - 1),
            (cell.row, cell.col + 1),
            (cell.row - 1, cell.col - 1),
            (cell.row - 1, cell.col),
            (cell.row - 1, cell.col + 1),
        ];

    internal static IEnumerable<(int, int)> Cells(this char[][] matrix) =>
        from row in Enumerable.Range(0, matrix.Length)
        from column in Enumerable.Range(0, matrix[row].Length)
        select (row, column);

    internal static List<string> Test =>
        [
            "..@@.@@@@.",
            "@@@.@.@.@@",
            "@@@@@.@.@@",
            "@.@@@@..@.",
            "@@.@@@@.@@",
            ".@@@@@@@.@",
            ".@.@.@.@@@",
            "@.@@@.@@@@",
            ".@@@@@@@@.",
            "@.@.@@@.@.",
        ];
}

internal static class Tests
{
    internal static void Test0()
    {
        var matrix = Solution.Test.ToMatrix();
        var cell = (0, 2);
        foreach (var neighbor in cell.Neighbors(matrix))
        {
            Console.WriteLine($"neighbor: {neighbor}\t({neighbor.At(matrix)})");
        }
    }
}
