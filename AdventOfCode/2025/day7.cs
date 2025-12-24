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

Console.WriteLine($"Part 1 (Expecting: ????): {Solution.SolvePart1()}");

Console.WriteLine($"Part 2 (Expecting: ????): {Solution.SolvePart2()}");

internal static class Solution
{
    private const int Year = 2025;
    private const int Day = 7;
    internal static string Source => $"Advent of Code - {Year}";
    internal static string Name => $"Day {Day}";
    internal static string FileName => $"./AdventOfCode/2025/data/day{Day}.txt";

    /*
    
    */
    internal static int SolvePart1(bool debug = false) =>
        (debug ? Test : FileName.ToLines()).ToMatrix().Length;

    /*
    
    */
    internal static int SolvePart2(bool debug = false) =>
        (debug ? Test : FileName.ToLines()).ToMatrix().Length;

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

    internal static List<string> Test =>
        [
            ".......S.......",
            ".......|.......",
            "......|^|......",
            "......|.|......",
            ".....|^|^|.....",
            ".....|.|.|.....",
            "....|^|^|^|....",
            "....|.|.|.|....",
            "...|^|^|||^|...",
            "...|.|.|||.|...",
            "..|^|^|||^|^|..",
            "..|.|.|||.|.|..",
            ".|^|||^||.||^|.",
            ".|.|||.||.||.|.",
            "|^|^|^|^|^|||^|",
            "|.|.|.|.|.|||.|",
        ];
}

internal static class Tests
{
    internal static void Test0() => Console.WriteLine(nameof(Test0));
}
