#!/usr/bin/env -S dotnet run

/*
974. Subarray Sums Divisible by K
https://leetcode.com/problems/subarray-sums-divisible-by-k/description/
*/

Console.WriteLine($"{Solution.ProblemName} Naive Approach");
Console.WriteLine($"Expect: {6}\tActual Count: {Solution.SolveNaive([3, 1, 2, 5, 1], 3)}");
Console.WriteLine($"Expect: {7}\tActual Count: {Solution.SolveNaive([4, 5, 0, -2, -3, 1], 5)}");
Console.WriteLine($"Expect: {0}\tActual Count: {Solution.SolveNaive([5], 9)}");
Console.WriteLine();

Console.WriteLine($"{Solution.ProblemName} Better Approach");
Console.WriteLine($"Expect: {6}\tActual Count: {Solution.Solve([3, 1, 2, 5, 1], 3)}");
Console.WriteLine($"Expect: {7}\tActual Count: {Solution.Solve([4, 5, 0, -2, -3, 1], 5)}");
Console.WriteLine($"Expect: {0}\tActual Count: {Solution.Solve([5], 9)}");
Console.WriteLine();

public class Solution
{
    public static string ProblemName => "Subarray Sums Divisible by K (# 974)";

    public static int Solve(int[] nums, int k)
    {
        int count = 0;
        int n = nums.Length;
        Dictionary<int, int> prefix = [];
        prefix[0] = 1;
        int sum = 0;

        foreach (int num in nums)
        {
            sum = ((sum + num) % k + k) % k;

            if (prefix.ContainsKey(sum))
            {
                count += prefix[sum];
            }

            if (prefix.ContainsKey(sum))
            {
                prefix[sum] += 1;
            }
            else
            {
                prefix[sum] = 1;
            }
        }

        return count;
    }

    /*
    Naive Version
    O(n^2) Time (nested loops)
    O(1) Space (constant memory usage, no extra data structures except original array)

    TODO: Implement prefix sum technique (see TwoSum and ThreeSum problems)

    */
    public static int SolveNaive(int[] array, int k)
    {
        int count = 0;
        int n = array.Length;

        for (int i = 0; i < n; i++)
        {
            int sum = 0;
            for (int j = i; j < n; j++)
            {
                sum += array[j];
                if (sum % k == 0)
                {
                    count += 1;
                }
            }
        }
        return count;
    }
}

internal static class DictionaryExtensions
{
    internal static void Print(this Dictionary<int, int> dictionary)
    {
        Console.WriteLine(new string('*', 80));
        Console.WriteLine("Dictionary Contents");
        Console.WriteLine(new string('*', 80));
        foreach (var (key, value) in dictionary)
        {
            Console.WriteLine($"{key}\t{value}");
        }
    }
}
