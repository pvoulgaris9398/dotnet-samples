#!/usr/bin/env -S dotnet run

#:project ./AdventOfCode/AdventOfCode.csproj

using System.Globalization;

Console.WriteLine($"{Solution.Source}: {Solution.Name}");

Console.WriteLine(new string('*', 80));

Console.WriteLine($"Part 1 (Expecting: 1139): {Solution.SolvePart1()}");

Console.WriteLine($"Part 2 (Expecting 6684): {Solution.SolvePart2()}");

internal static class Solution
{
    internal const int K = 100;

    /*
    Not pretty but it works!
    TODO: Generalize the Part 2 answer

    42 => wrong
    51 => wrong
    1139 => correct!

    */
#pragma warning disable IDE0060 // Remove unused parameter
    public static int SolvePart1(bool debug = false)
    {
        var current = 50;
        var count = 0;

        var lines = ReadFromFile(FileName).ReadLines();

        foreach (Rotation rotation in lines.Rotations())
        {
            current = rotation.Rotate(current);
            if (current == 0)
            {
                count += 1;
            }
        }
        return count;
    }

    /*
    Brute force and not elegant, but it works!

    6684 => correct!

    */
    public static int SolvePart2(bool debug = false)
    {
        var current = 50;
        var count = 0;
        var totalCount = 0;

        var lines = ReadFromFile(FileName).ReadLines();

        foreach (Rotation rotation in lines.Rotations())
        {
            (current, count) = rotation.CompleteRevolutionCount(current);
            totalCount += count;
        }
        return totalCount;
    }

    private const int Year = 2025;
    private const int Day = 1;
    internal static string Source => $"Advent of Code - {Year}";
    internal static string Name => $"Day {Day}";
    internal static string FileName => $"./AdventOfCode/2025/data/day{Day}.txt";

    internal static List<string> Lines => [.. File.OpenText(FileName).ReadLines()];

    internal static IEnumerable<string> ReadLines(this TextReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        while (reader.ReadLine() is string line)
        {
            yield return line;
        }
    }

    internal static TextReader ReadFromFile(string path) => File.OpenText(path);

    internal static IEnumerable<Rotation> Rotations(this IEnumerable<string> lines) =>
        lines.Where(line => !string.IsNullOrWhiteSpace(line)).Select(ToRotation);

    internal static Rotation ToRotation(string line)
    {
        return line switch
        {
            { Length: > 1 }
                when line[0] == 'R' && int.TryParse(line[1..].ToString(), out int number) =>
                new Clockwise(number),
            { Length: > 1 }
                when line[0] == 'L' && int.TryParse(line[1..].ToString(), out int number) =>
                new CounterClockwise(number),
            _ => throw new ArgumentException("Invalid data!"),
        };
    }
}

internal static class RotationExtensions
{
    internal const int K = 100;
    extension(Rotation rotation)
    {
        internal int Rotate(int current) => (current + K + rotation.Clicks) % K;

        internal (int, int) CompleteRevolutionCount(int current, bool debug = false)
        {
            int count = 0;
            int sign = rotation.Clicks < 0 ? (-1) : (+1);
            int index = 0;
            while (index < Math.Abs(rotation.Clicks))
            {
                if (debug)
                {
                    Console.WriteLine($"[index] = {index}\tCurrent:\t{current}");
                }

                current = (current + K + (1 * sign)) % K;

                index++;
                if (current == 0)
                {
                    count += 1;
                }
            }
            return (current, count);
        }
    }
}

internal record Rotation(int Clicks);

internal sealed record CounterClockwise(int Clicks) : Rotation(-Clicks);

internal sealed record Clockwise(int Clicks) : Rotation(Clicks);

internal static class FirstAttempt
{
    public static int SolvePart1(bool debug = false)
    {
        var current = 50;
        var count = 0;

        var lines = Solution.ReadFromFile(Solution.FileName).ReadLines();

        foreach (var (direction, number) in lines.Rotations1())
        {
            current = NextLocation(current, (direction == 'R' ? (+1) : (-1)) * number);
            if (debug)
                Console.WriteLine($"Current: {current}");
            if (current == 0)
            {
                count += 1;
            }
        }
        return count;
    }

    public static int NextLocation(int start, int number) =>
        /*
Adding "K" after "start" (before the modulus operator) was the key
to getting the counts to properly wrap-around
*/
        (start + Solution.K + number) % Solution.K;

    internal static IEnumerable<(char, int)> Rotations1(this IEnumerable<string> lines) =>
        lines.Select(ToRotation1);

    internal static (char, int) ToRotation1(string line)
    {
        char direction = line[0];
        string number = line[1..];
        return (direction, int.Parse(number, CultureInfo.InvariantCulture));
    }
}
