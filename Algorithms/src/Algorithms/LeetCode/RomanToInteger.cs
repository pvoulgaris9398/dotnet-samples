#!/usr/bin/env -S dotnet run

Console.WriteLine("{0} = {1}, (Expected: {2})", "III", Solution.RomanToInt("III"), 3);
Console.WriteLine("{0} = {1}, (Expected: {2})", "LVIII", Solution.RomanToInt("LVIII"), 58);
Console.WriteLine("{0} = {1}, (Expected: {2})", "MCMXCIV", Solution.RomanToInt("MCMXCIV"), 1994);

/*
https://leetcode.com/problems/roman-to-integer/
*/
public class Solution
{
    public static int RomanToInt(string input)
    {
        return Values(input).Sum();
    }

    public static IEnumerable<int> Values(string input)
    {
        char[] characters = [.. input];

        if (characters.Length == 0) yield return 0;

        char current = characters[0];

        for (int i = 1; i < characters.Length; i++)
        {
            var next = characters[i];

            var currentValue = GetValue(current);
            var nextValue = GetValue(next);

            yield return ((currentValue < nextValue) ? (-1) : (+1)) * currentValue;

            current = next;
        }

        yield return GetValue(current);

    }

    private static int GetValue(char numeral) => numeral switch
    {
        'I' => 1,
        'V' => 5,
        'X' => 10,
        'L' => 50,
        'C' => 100,
        'D' => 500,
        'M' => 1000,
        _ => throw new NotSupportedException()
    };
}