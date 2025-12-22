#!/usr/bin/env -S dotnet run

using System.Text;

/*
These look a lot like sequences, such as x -> x*x or x-> x+x :-)
*/

int[] array = [1, 2, 3, 4, 5, 6];
Console.WriteLine(
    $"Prefix sum array for: {array.Print()} is {Solution.CreatePrefixSumArray(array).Print()}"
);

Console.WriteLine(
    $"Prefix product array for: {array.Print()} is {Solution.CreatePrefixProductArray(array).Print()}"
);

public class Solution
{
    public static int[] CreatePrefixProductArray(int[] array)
    {
        int len = array.Length;
        int[] prefix = new int[len];

        prefix[0] = array[0];

        for (int i = 1; i < len; i++)
        {
            prefix[i] = prefix[i - 1] * array[i];
        }
        return prefix;
    }

    public static int[] CreatePrefixSumArray(int[] array)
    {
        int len = array.Length;
        int[] prefix = new int[len];

        prefix[0] = array[0];

        for (int i = 1; i < len; i++)
        {
            prefix[i] = prefix[i - 1] + array[i];
        }
        return prefix;
    }
}

public static class ArrayExtensions
{
    public static string Print(this int[] array)
    {
        return $"[{string.Join(',', array)}]";
        // StringBuilder sb = new StringBuilder();
        // sb.Append("[");
        // for (int i = 0; i < array.Length; i++)
        // {
        //     // Console.WriteLine($"i = {i}\tValue = {array[i]}");
        //     // sb.Append(string.Join())
        //     sb.Append($"{array[i]} ,");
        // }
        // sb.Append("]");
        // return sb.ToString();
    }
}
