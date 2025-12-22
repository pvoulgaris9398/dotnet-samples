#!/usr/bin/env -S dotnet run

Console.WriteLine(
    "Longest Substring Actual: {0}, Expected: {1}",
    Solution.LengthOfLongestSubstring("abcabcbb"),
    3
);

Console.WriteLine(
    "Longest Substring Actual: {0}, Expected: {1}",
    Solution.LengthOfLongestSubstring("bbbbb"),
    1
);

Console.WriteLine(
    "Longest Substring Actual: {0}, Expected: {1}",
    Solution.LengthOfLongestSubstring("pwwkew"),
    3
);

Console.WriteLine(
    "Longest Substring Actual: {0}, Expected: {1}",
    Solution.LengthOfLongestSubstring(""),
    0
);

Console.WriteLine(
    "Longest Substring Actual: {0}, Expected: {1}",
    Solution.LengthOfLongestSubstring(" "),
    1
);

Console.WriteLine(
    "Longest Substring Actual: {0}, Expected: {1}",
    Solution.LengthOfLongestSubstring("dvdf"),
    3
);

/*
https://leetcode.com/problems/longest-substring-without-repeating-characters/

- Using sliding "window" technique, with two (2) pointers
- Keep track of characters currently in window
- Expand the window to the right one step at a time
- If a character is NOT in the set, add it and increment the right pointer
- If a character IS in the set, this is a duplicate
- Remove elements from the set, by incrementing the left pointer until the current value
is no longer in the window
- The next iteration of the while loop will re-add the "next" variable and continue on
- Keep track of longest sequence seen so far
- When right pointer is at the end of the input string
- Return the current longest value

*/
#pragma warning disable CA1050 // Declare types in namespaces
public class Solution
#pragma warning restore CA1050 // Declare types in namespaces
{
    public static int LengthOfLongestSubstring(string s)
    {
        if (s == null)
            return 0;

        char[] chars = s!.ToCharArray();

        int length = chars.Length;
        int left = 0;
        int right = 0;
        HashSet<char> window = [];
        int maxLength = 0;

        while (left < length && right < length)
        {
            char next = chars[right];
            if (window.Add(next))
            {
                maxLength = window.Count > maxLength ? window.Count : maxLength;
                right++;
            }
            else
            {
                while (window.Contains(next))
                {
                    window.Remove(chars[left]);
                    left++;
                }
            }
        }
        return maxLength;
    }

    // Doesn't work...
    internal static IEnumerable<int> RunLengthsOfUniqueCharsV2(string s)
    {
        if (s == null)
            yield return 0;

        char[] chars = s!.ToCharArray();

        HashSet<char> used = [];

        foreach (var next in chars)
        {
            if (used.Contains(next))
            {
                yield return used.Count;
                used.Clear();
            }
            used.Add(next);
        }
        yield return used.Count;
    }

    // Doesn't work...
    public static int LengthOfLongestSubstringV1(string s)
    {
        /*
        Convert input string to char array
        If s is null or whitespace return 0
        Initialize longestSoFar to zero
        Initialize counter to zero
        Initialize empty hashset to keep track of "used" chars
        Iterate over each char
        if not in hashset
        Increment counter, add to hashset
        else if in hashset
        check if counter > longestSoFar, update if yes, otherwise not
        reset counter
        move to next element
        start over
        loop
        */
        if (string.IsNullOrEmpty(s))
            return 0;

        char[] chars = s.ToCharArray();
        int currentLength = 1;
        int longestSoFar = 0;
        var used = new HashSet<char>();
        char prev = chars[0];
        for (int i = 0; i < chars.Length; i++)
        {
            if (used.Add(chars[i]))
            {
                currentLength += 1;
            }
            else
            {
                // Update the current longest value, if needed
                longestSoFar = currentLength > longestSoFar ? currentLength : longestSoFar;
                // Reset
                currentLength = 0;
                used.Clear();

                // Add the prev char back into the list and account for it
                if (prev == chars[i])
                {
                    used.Add(chars[i]);
                    currentLength += 1;
                }
                else
                {
                    prev = chars[i];
                }
            }
        }
        return longestSoFar;
        /*

        ArraySegment<char> soFar = new ArraySegment<char>(chars, 0, chars.Length);

        var slice = soFar.Slice()

        */
    }
}
