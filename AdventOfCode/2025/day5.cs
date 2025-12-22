#!/usr/bin/env -S dotnet run

using System;
using System.Collections.Generic;
using static Extensions;

Console.WriteLine($"{Solution.Source}: {Solution.Name}");

Console.WriteLine(new string('*', 80));

Console.WriteLine($"Part 1 (Expecting: 828): {Solution.SolvePart1()}");

Console.WriteLine($"Part 2 (Expecting: 352,681,648,086,146): {Solution.SolvePart2():#,##0}");

// Tests.Test1();
// Tests.Test2();

public static class Solution
{
    public static long SolvePart1(bool debug = false)
    {
        var data = debug ? Test : Lines;

        var ranges = data.ToRanges();

        var ingredients = data.ToIngredients();

        return ingredients.Count(ingredient => ingredient.IsFresh(ranges));
    }

    public static long SolvePart2(bool debug = false)
    {
        var data = debug ? Test : Lines;

        return data.ToRanges().Merge().Sum(range => range.RangeCount);
    }

    internal static IEnumerable<string> ReadLines(this TextReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        while (reader.ReadLine() is string line)
        {
            yield return line;
        }
    }

    internal static List<string> Lines => [.. File.OpenText(FileName).ReadLines()];
    private const int Year = 2025;
    private const int Day = 5;
    internal static string Source => $"Advent of Code - {Year}";
    internal static string Name => $"Day {Day}";
    internal static string FileName => $"./data/day{Day}.txt";
    internal static List<string> Test =>
        ["3-5", "10-14", "16-20", "12-18", "", "1", "5", "8", "11", "17", "32"];
}

internal static class Extensions
{
    internal static IEnumerable<FreshRange> ToRanges(this IEnumerable<string> lines)
    {
        foreach (
            string line in lines.Where(line =>
                !string.IsNullOrWhiteSpace(line) && line.Contains('-')
            )
        )
        {
            FreshRange parsed = line.Split('-') switch
            {
                [string first, string second]
                    when long.TryParse(first, out long lower)
                        && long.TryParse(second, out long upper) => new(lower, upper),
                _ => throw new InvalidDataException(),
            };
            yield return parsed;
        }
    }

    internal static IEnumerable<Ingredient> ToIngredients(this IEnumerable<string> lines) =>
        lines
            .Where(line => !line.Contains('-') && !string.IsNullOrWhiteSpace(line))
            .Select(line => new Ingredient(long.Parse(line)));

    internal static bool IsFresh(this Ingredient ingredient, IEnumerable<FreshRange> ranges) =>
        ranges.Any(range => ingredient >= range.LowerBound && ingredient <= range.UpperBound);

    internal static IEnumerable<FreshRange> Merge(this IEnumerable<FreshRange> ranges)
    {
        var sorted = ranges.OrderBy(range => range.LowerBound);

        IEnumerator<FreshRange> enumerator = sorted.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            yield break;
        }

        (long currLowerBound, long currUpperBound) = enumerator.Current;

        while (enumerator.MoveNext())
        {
            FreshRange current = enumerator.Current;

            if (current.LowerBound > currUpperBound)
            {
                yield return new(currLowerBound, currUpperBound);
                (currLowerBound, currUpperBound) = enumerator.Current;
                continue;
            }
            currUpperBound = Math.Max(currUpperBound, current.UpperBound);
        }
        yield return new(currLowerBound, currUpperBound);
    }

    internal record Ingredient(long Value)
    {
        public static implicit operator Ingredient(long value) => new(value);

        public static bool operator >=(Ingredient left, Ingredient right) =>
            left.Value >= right.Value;

        public static bool operator <=(Ingredient left, Ingredient right) =>
            left.Value <= right.Value;
    }

    internal record FreshRange(long LowerBound, long UpperBound)
    {
        public long RangeCount => UpperBound - LowerBound + 1;
    }

    internal static class Tests
    {
        internal static void Test1()
        {
            var ranges = Solution.Test.ToRanges();

            foreach (FreshRange range in ranges)
            {
                Console.WriteLine($"{range}");
            }

            var ingredients = Solution.Test.ToIngredients();

            foreach (Ingredient ingredient in ingredients)
            {
                Console.WriteLine($"{ingredient}");
            }

            var freshCount = ingredients.Count(ingredient => ingredient.IsFresh(ranges));
            Console.WriteLine($"{nameof(freshCount)}: '{freshCount}'");
        }

        internal static void Test2()
        {
            var ranges = Solution.Test.ToRanges();

            var rangeCount = ranges.Merge().Sum(range => range.RangeCount);
            Console.WriteLine($"{nameof(rangeCount)}: '{rangeCount}'");
        }
    }
}
