#!/usr/bin/env -S dotnet run

if (args.Length > 0)
{
    Action actionToExecute = args[0] switch
    {
        "test0" => Tests.Test0,
        "test1" => Tests.Test1,
        "test2" => Tests.Test2,
        _ => () => Console.WriteLine("Unrecognized command-line argument"),
    };
    actionToExecute();
    return;
}

internal static class Solution
{
    internal static void GeneratePermutationsFor<T>(List<List<T>> result, T[] input, int idx)
    {
        if (idx == input.Length)
        {
            result.Add([.. input]);
        }
        for (int i = idx; i < input.Length; i++)
        {
            // Swap
            (input[idx], input[i]) = (input[i], input[idx]);

            // Recurse
            GeneratePermutationsFor(result, input, idx + 1);

            // Backtrack
            (input[i], input[idx]) = (input[idx], input[i]);
        }
    }

    internal static void GeneratePermutationsFor(List<List<int>> result, int[] input, int idx)
    {
        if (idx == input.Length)
        {
            result.Add([.. input]);
        }
        for (int i = idx; i < input.Length; i++)
        {
            // Swap
            (input[idx], input[i]) = (input[i], input[idx]);

            // Recurse
            GeneratePermutationsFor(result, input, idx + 1);

            // Backtrack
            (input[i], input[idx]) = (input[idx], input[i]);
        }
    }

    internal static List<List<int>> GeneratePermutationsFor(int[] input)
    {
        List<List<int>> result = [];
        GeneratePermutationsFor(result, input, 0);
        return result;
    }

    internal static List<List<T>> GeneratePermutationsFor<T>(T[] input)
    {
        List<List<T>> result = [];
        GeneratePermutationsFor(result, input, 0);
        return result;
    }
}

internal static class Tests
{
    internal static void Test0()
    {
        var result = Solution.GeneratePermutationsFor([1, 2, 3]);
        result.Print();
    }

    internal static void Test1()
    {
        char[] input = ['a', 'b', 'c', 'd'];
        var result = Solution.GeneratePermutationsFor(input);
        Console.WriteLine($"Number of Permutations = {result.Count}");
        result.Print();
    }

    internal static void Test2()
    {
        int n = 5;
        Console.WriteLine($"Factorial of: '{n} is '{Combinatorics.Factorial(n)}'");
    }
}

internal static class Combinatorics
{
    internal static int Permutations(int n, int r = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(r, 1, nameof(r));
        ArgumentOutOfRangeException.ThrowIfLessThan(n, 0, nameof(r));
        return Factorial(n) / Factorial(n - r);
    }

    internal static int Factorial(int n)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(n, 0, nameof(n));

        if (n == 0)
        {
            return 1;
        }

        int result = 1;
        while (n != 1)
        {
            result *= n;
            n -= 1;
        }
        return result;
    }
}

// TODO: Move to the "Common" assembly and reference from there
internal static class ListExtensions
{
    public static void Print<T>(this List<List<T>> input)
    {
        foreach (List<T> row in input)
        {
            foreach (T column in row)
            {
                Console.Write($"{column} ");
            }
            Console.WriteLine();
        }
    }
}
