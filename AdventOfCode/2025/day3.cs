#!/usr/bin/env -S dotnet run

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

Console.WriteLine($"{Solution.Source}: {Solution.Name}");

Console.WriteLine(new string('*', 80));

Console.WriteLine($"Part 1 (Expecting: 17376): {Solution.SolvePart1(true)}");

Tests.Test5();

//Console.WriteLine($"Part 2 (Expecting: ????): {Solution.SolvePart2()}");

public static class Solution
{
    /*
    17376 => correct
    */
    public static long SolvePart1(bool debug = false)
    {
        return (debug ? Test : Lines).ToBanks(2).Sum(b => b.Joltage);
    }

    /*
    TODO:
    */
    public static long SolvePart2(bool debug = false)
    {
        return (debug ? Test : Lines).ToBanks(12).Sum(b => b.Joltage);
    }

    private const int Year = 2025;
    internal static string Source => $"Advent of Code - {Year}";
    internal static string Name => "Day 3";
    internal static string FileName => $"./data/day3.txt";

    internal static List<string> Test =>
        ["987654321111111", "811111111111119", "234234234234278", "818181911112111"];

    internal static List<string> Lines => [.. File.OpenText(FileName).ReadLines()];

    internal static IEnumerable<string> ReadLines(this TextReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        while (reader.ReadLine() is string line)
        {
            yield return line;
        }
    }

    internal static IEnumerable<Bank> ToBanks(this IEnumerable<string> lines, int K) =>
        lines.Select(line => line.ToBank(K));

    internal static Bank ToBank(this string line, int K) =>
        new(line.ToCharArray().ToBatteries().ToList(), K);

    internal static IEnumerable<Battery> ToBatteries(this char[] batteries) =>
        batteries.Select((b, i) => new Battery(long.Parse(b.ToString()), i));
}

internal static class Tests
{
    internal static void Test0()
    {
        foreach (Bank bank in (Solution.Test).ToBanks(2))
        {
            Console.WriteLine(bank);
        }
    }

    internal static void Test1()
    {
        Bank testBank = new(
            [
                new(8, 0),
                new(4, 1),
                new(3, 2),
                new(5, 3),
                new(4, 4),
                new(9, 5),
                new(1, 6),
                new(3, 7),
                new(1, 8),
                new(3, 9),
            ],
            2
        );

        foreach (var highest in testBank.TwoHighestValues())
        {
            Console.WriteLine(highest);
        }
    }

    internal static void Test2()
    {
        // 234 234 234 234 278
        Bank testBank = new(
            [
                new(2, 0),
                new(3, 1),
                new(4, 2),
                new(2, 3),
                new(3, 4),
                new(4, 5),
                new(2, 6),
                new(3, 7),
                new(4, 8),
                new(2, 9),
                new(7, 10),
                new(8, 11),
            ],
            2
        );

        var highest = BankExtensions.TwoHighestValues(testBank).ToList();

        Console.WriteLine(highest[0]);
        Console.WriteLine(highest[1]);
    }

    internal static void Test3()
    {
        // string input = "987654321111111";
        // string input = "811111111111119";
        // string input = "234234234234278";
        string input = "818181911112111";
        int n = input.Length;
        int i = 0;
        char s1 = '\0';
        char s2 = '\0';

        while (i < n - 1)
        {
            if (input[i] > s1)
            {
                s1 = input[i];
                s2 = '\0';
            }
            if (input[i + 1] > s2)
            {
                s2 = input[i + 1];
            }
            i++;
        }

        Console.WriteLine($"({s1}, {s2})");
    }

    internal static void Test4()
    {
        foreach (Bank bank in Solution.Test.ToBanks(2))
        {
            var highest = BankExtensions.TwoHighestValues(bank).ToList();
            Console.WriteLine($"{highest[0]}{highest[1]}");
        }
    }

    internal static void Test5()
    {
        // string input = "987654321111111";
        // string input = "811111111111119";
        // string input = "234234234234278";
        string input = "818181911112111";
        // int k = 2;
        var newBank = new Bank([.. input.ToCharArray().ToBatteries()], 2);
        Console.WriteLine(newBank.KHighestValue(input, 2));
    }

    internal static void RunAll()
    {
        Test0();
        Test1();
        Test2();
        Test3();
        Test4();
        Test5();
    }
}

internal static class BankExtensions
{
    extension(Bank bank)
    {
        internal long Joltage
        {
            get
            {
                // Maintain order of occurrence of the batteries, just in case
                int[] highest = [66]; //KHighestValuesInOrder(bank, bank.K).ToList();

                // Console.WriteLine(highest[0]);
                // Console.WriteLine(highest[1]);

                return long.Parse(string.Join("", highest.ToList()));
            }
        }
    }

    internal static long KHighestValue(this Bank bank, string input, int k)
    {
        int len = input.Length;

        if (len < k)
            return -1;

        if (len == k)
            return long.Parse(input);

        return 42;
    }

    internal static IEnumerable<long> KHighestValuesInOrder_ORIGINAL(this Bank bank, int k)
    {
        PriorityQueue<Battery, long> maxQueue = new();

        var batteries = bank.Batteries.ToList();
        int i = 0;
        int n = bank.Batteries.Count;

        while (i < n)
        {
            maxQueue.Enqueue(batteries[i], batteries[i].Joltage);
            if (maxQueue.Count > k)
            {
                maxQueue.Dequeue();
            }
            i++;
        }

        List<Battery> result = [];

        foreach (var (battery, priority) in maxQueue.UnorderedItems)
        {
            result.Add(battery);
        }

        foreach (var battery in result.OrderBy(battery => battery.Order))
        {
            Console.Write($"{battery.Joltage}");
            yield return battery.Joltage;
        }
    }

    internal static IEnumerable<int> TwoHighestValues(this Bank bank)
    {
        string input = string.Join(
            "",
            bank.Batteries.Select(battery => battery.Joltage.ToString())
        );
        int n = input.Length;
        int i = 0;
        char s1 = '\0';
        char s2 = '\0';

        // Console.WriteLine(input);

        while (i < n - 1)
        {
            if (input[i] > s1)
            {
                s1 = input[i];
                s2 = '\0';
            }
            if (input[i + 1] > s2)
            {
                s2 = input[i + 1];
            }
            i++;
        }
        yield return int.Parse(s1.ToString());
        yield return int.Parse(s2.ToString());
    }
}

internal record Bank(List<Battery> Batteries, int K);

internal record Battery(long Joltage, long Order);
