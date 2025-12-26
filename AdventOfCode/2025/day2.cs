#!/usr/bin/env -S dotnet run

#:project ./AdventOfCode/AdventOfCode.csproj

// Use GeneratedRegexAttribute to generate the regular expression implementation at compile time.
#pragma warning disable SYSLIB1045

using System.Globalization;
using System.Text.RegularExpressions;

if (args.Length > 0)
{
    Action actionToExecute = args[0] switch
    {
        "test0" => Tests.Test0,
        "test1" => Tests.Test1,
        "test2" => Tests.Test2,
        "test3" => Tests.Test3,
        "test4" => Tests.Test4,
        "test5" => Tests.Test5,
        "test6" => Tests.Test6,
        "runall" => Tests.RunAll,
        _ => () => Console.WriteLine("Unrecognized command-line argument"),
    };
    actionToExecute();
    return;
}

Console.WriteLine($"{Solution.Source}: {Solution.Name}");

Console.WriteLine(new string('*', 80));

Console.WriteLine($"Part 1 (Expecting: 5398419778): {Solution.SolvePart1()}");

Console.WriteLine($"Part 2 (Expecting: 15704845910): {Solution.SolvePart2()}");

internal static class Solution
{
    /*
    903121192024591 => wrong, too high
    5398419778 => correct
    */
    internal static long SolvePart1(bool debug = false)
    {
        return (debug ? Test : Raw)
            .ToRanges()
            .Sum(range => range.InvalidIDs(RangeExtensions.IsNotValidV1).Sum());
    }

    /*

    6501460057 => wrong, too low
    15704845910 => correct, after getting a hint on the correct regex configuration

    */
    internal static long SolvePart2(bool debug = false)
    {
        return (debug ? Test : Raw)
            .ToRanges()
            .Sum(range => range.InvalidIDs(RangeExtensions.IsNotValidV2).Sum());
    }

    /*
    Part 1 (only):
    From problem statement of what constitutes an invalid ID:
    "any ID which is made only of some sequence of digits repeated twice"

    Therefore an "odd" number of digits is automatically valid

    */

    private const int Year = 2025;
    private const int Day = 2;
    internal static string Source => $"Advent of Code - {Year}";
    internal static string Name => $"Day {Day}";
    internal static string FileName => $"./AdventOfCode/2025/data/day{Day}.txt";
    internal static List<string> Raw => [.. File.ReadAllText(FileName).Split(",")];
    internal static List<string> Test => [.. TestData.Split(",")];
    internal const string TestData =
        "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
}

internal static class Tests
{
    internal static void Test0()
    {
        foreach (var range in Solution.Raw.ToRanges())
        {
            Console.WriteLine(range);
        }
    }

    internal static void Test1()
    {
        long value1 = 1010;
        Console.WriteLine($"{nameof(value1)}: {value1} {RangeExtensions.IsNotValidV1(value1)}");
    }

    internal static void Test2()
    {
        long value2 = 12;
        Console.WriteLine($"{nameof(value2)}: {value2} {RangeExtensions.IsNotValidV1(value2)}");
    }

    internal static void Test3()
    {
        var results = Solution
            .Test.ToRanges()
            .SelectMany(range => range.InvalidIDs(RangeExtensions.IsNotValidV1));

        foreach (var entry in results)
        {
            Console.WriteLine(entry);
        }
    }

    internal static void Test4()
    {
        Console.WriteLine($"2121212121: {RangeExtensions.IsNotValidV2(2121212121)}");
        Console.WriteLine($"121212: {RangeExtensions.IsNotValidV2(121212)}");
        Console.WriteLine($"77: {RangeExtensions.IsNotValidV2(77)}");
        Console.WriteLine($"123: {RangeExtensions.IsNotValidV2(123)}");
        Console.WriteLine($"121224: {RangeExtensions.IsNotValidV2(121224)}");
    }

    internal static void Test5()
    {
        var results = Solution
            .Test.ToRanges()
            .SelectMany(range => range.InvalidIDs(RangeExtensions.IsNotValidV2));

        foreach (var entry in results)
        {
            Console.WriteLine(entry);
        }
    }

    internal static void Test6() =>
        Console.WriteLine($"2121212121: {RangeExtensions.IsNotValidV2(2121212121)}");

    internal static void RunAll()
    {
        Test0();
        Test1();
        Test2();
        Test3();
        Test4();
        Test5();
        Test6();
    }
}

internal static class RangeExtensions
{
    public static bool IsNotValidV2(long value)
    {
        string s = value.ToString(CultureInfo.InvariantCulture);
        int len = s.Length;

        if (len < 2)
        {
            return false;
        }

        // Regex regex = new Regex(@"(.+)\1"); // <== BAD
        Regex regex = new(@"^(.+?)\1+$"); // <== GOOD

        MatchCollection matches = regex.Matches(s);

        return matches.Any(match => match.Value.Length == len);
    }

    public static bool IsNotValidV1(long value)
    {
        string s = value.ToString(CultureInfo.InvariantCulture);

        if (s.Length % 2 != 0)
            return false;

        int mid = s.Length / 2;
        string first = s[..mid];
        string second = s[mid..];

        return (first, second) switch
        {
            ({ Length: > 0 } left, { Length: > 0 } right)
                when left.Equals(right, StringComparison.Ordinal) => true,
            _ => false,
        };
    }

    internal static IEnumerable<Range> ToRanges(this IEnumerable<string> lines) =>
        lines.Select(line => line.ToRange());

    internal static Range ToRange(this string line)
    {
        return line.Split("-") switch
        {
            [string first, string second]
                when long.TryParse(first.ToString(), out long start)
                    && long.TryParse(second.ToString(), out long end) => new Range(start, end),
            _ => throw new ArgumentException("Invalid data!"),
        };
    }

    internal static IEnumerable<long> InvalidIDs(this Range range, Func<long, bool> isNotValid)
    {
        for (long i = range.Start; i <= range.End; i++)
        {
            if (isNotValid(i))
            {
                yield return i;
            }
        }
    }

    extension(Range range) { }
}

internal sealed record Range(long Start, long End);
