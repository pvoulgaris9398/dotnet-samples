#!/usr/bin/env -S dotnet run

using System.Text;

/*
1768. Merge Strings Alternately
*/

Console.WriteLine($"{Solution.MergeAlternately("abc", "pqr")}");

Console.WriteLine($"{Solution.MergeAlternately("ab", "pqrs")}");

Console.WriteLine($"{Solution.MergeAlternately("abcd", "pq")}");

Console.WriteLine($"{Solution.MergeAlternatelyV2("abc", "pqr")}");

Console.WriteLine($"{Solution.MergeAlternatelyV2("ab", "pqrs")}");

Console.WriteLine($"{Solution.MergeAlternatelyV2("abcd", "pq")}");

public class Solution
{
    /*
    Interesting alternative approach I've seen
    */
    public static string MergeAlternatelyV2(string word1, string word2)
    {
        int length1 = word1.Length;
        int length2 = word2.Length;
        int index1 = 0;
        int index2 = 0;

        StringBuilder sb = new StringBuilder();

        while (index1 < length1 || index2 < length2)
        {
            if (index1 < length1)
            {
                sb.Append(word1[index1]);
                index1 += 1;
            }
            if (index2 < length2)
            {
                sb.Append(word2[index2]);
                index2 += 1;
            }
        }

        return sb.ToString();
    }
    public static string MergeAlternately(string word1, string word2)
    {
        char[] merged = new char[word1.Length + word2.Length];

        int sourceIndex = 0;
        int destinationIndex = 0;

        while (sourceIndex < word1.Length && sourceIndex < word2.Length)
        {
            merged[destinationIndex] = word1[sourceIndex];
            destinationIndex += 1;

            merged[destinationIndex] = word2[sourceIndex];
            destinationIndex += 1;

            sourceIndex += 1;
        }

        while (sourceIndex < word1.Length)
        {
            merged[destinationIndex] = word1[sourceIndex];
            destinationIndex += 1;
            sourceIndex += 1;
        }

        while (sourceIndex < word2.Length)
        {
            merged[destinationIndex] = word2[sourceIndex];
            destinationIndex += 1;
            sourceIndex += 1;
        }

        return new string(merged);
    }
}