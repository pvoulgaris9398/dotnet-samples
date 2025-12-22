#!/usr/bin/env -S dotnet run

#:project ./AdventOfCode/AdventOfCode.csproj

using AdventOfCode;

// Tests.Test0(Solution.Test.ToMatrix());

Console.WriteLine($"Part 1 (Expecting: ????): {Solution.SolvePart1()}");

Console.WriteLine($"Part 2 (Expecting: ????): {Solution.SolvePart2()}");

public static class Solution
{
    internal static string Name => "Day 7";
    internal static string FileName => "./AdventOfCode2025/data/day7.txt";

    /*
    
    */
    internal static int SolvePart1(bool debug = false)
    {
        return (debug ? Test : FileName.ToLines()).ToMatrix().Count();
    }

    /*
    
    */
    internal static int SolvePart2(bool debug = false)
    {
        return (debug ? Test : FileName.ToLines()).ToMatrix().Count();
    }

    internal static IEnumerable<(int, int)> Neighbors(
        this (int row, int col) cell,
        char[][] matrix
    ) =>
        new[]
        {
            (cell.row + 1, cell.col - 1),
            (cell.row + 1, cell.col),
            (cell.row + 1, cell.col + 1),
            (cell.row, cell.col - 1),
            (cell.row, cell.col + 1),
            (cell.row - 1, cell.col - 1),
            (cell.row - 1, cell.col),
            (cell.row - 1, cell.col + 1),
        };

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
    internal static void Test0(char[][] matrix)
    {
        Console.WriteLine(nameof(Test0));
    }
}
