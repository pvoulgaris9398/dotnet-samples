#!/usr/bin/env -S dotnet run

/*
Time Complexity: O(n2)
Space Complexity: O(1) (No additional allocations required)
*/
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

internal static class BubbleSort
{
    internal static void Sort(int[] input)
    {
        if (input is { Length: > 0 })
        {
            int n = input.Length;
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (input[j] > input[j + 1])
                    {
                        (input[j], input[j + 1]) = (input[j + 1], input[j]);
                        swapped = true;
                    }
                }
                if (!swapped)
                {
                    break;
                }
            }
        }
    }
}

internal static class Tests
{
    internal static void Test0()
    {
        int[] input = [1, 344, 222, 666, 11, 25, 12, 444, 6, 23, 65, 2];

        Console.WriteLine("Before:");
        input.Print();

        BubbleSort.Sort(input);

        Console.WriteLine("After:");
        input.Print();
    }
}

// TODO: Move to the "Common" assembly and reference from there
internal static class ListExtensions
{
    public static void Print(this int[] input)
    {
        foreach (int element in input)
        {
            Console.Write($"{element},");
        }
        Console.WriteLine();
    }
}
