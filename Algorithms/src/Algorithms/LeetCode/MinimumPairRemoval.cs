#!/usr/bin/env -S dotnet run

/*
https://leetcode.com/problems/minimum-pair-removal-to-sort-array-i/
*/

if (args.Length > 0)
{
    Action actionToExecute = args[0] switch
    {
        "test0" => () => Tests.Test0([5, 2, 3, 1]),
        "test1" => () => Tests.Test0([1, 2, 2]),
        "test2" => () => Tests.Test0([-2, 1, 2, -1, -1, -2, -2, -1, -1, 1, 1]),
        _ => () => Console.WriteLine("Unrecognized command-line argument"),
    };
    actionToExecute();
    return;
}

internal static class Solution
{
    internal static int MinimumPairRemoval(int[] nums)
    {
        var list = nums.ToList(); // Allocations
        int count = 0;

        Console.WriteLine(new string('*', 80));
        Print(list);

        while (!IsNonDecreasing(list))
        {
            (int first, int second) = GetIndicesOfMinPair(list);

            int sum = list[first] + list[second];
            list[first] = sum;
            list.RemoveAt(second);
            count++;

            Console.WriteLine(new string('*', 80));
            Print(list);
        }
        return count;
    }

    internal static void Print<T>(IEnumerable<T> source)
    {
        foreach (T item in source)
        {
            Console.WriteLine($"{typeof(T)}: {item}");
        }
    }

    internal static (int, int) GetIndicesOfMinPair(IReadOnlyList<int> source)
    {
        //var list = new List<int>(source); // Allocations
        int n = source.Count;

        if (n < 2)
        {
            return (-1, -1);
        }

        int minSum = int.MaxValue;
        int minIndex = -1;

        for (int i = 0; i < n - 1; i++)
        {
            int currentSum = source[i] + source[i + 1];
            if (currentSum < minSum)
            {
                minSum = currentSum;
                minIndex = i;
            }
        }
        return (minIndex, minIndex + 1);
    }

    internal static bool IsNonDecreasing(IReadOnlyList<int> source)
    {
        //var list = new List<int>(source); // Allocations
        int n = source.Count;

        if (n < 2)
        {
            return true;
        }

        for (int i = 0; i < n - 1; i++)
        {
            if (source[i] > source[i + 1])
            {
                return false;
            }
        }
        return true;
    }
}

internal static class Tests
{
    internal static void Test0(int[] nums) =>
        Console.WriteLine($"Result: {Solution.MinimumPairRemoval(nums)}");
}
