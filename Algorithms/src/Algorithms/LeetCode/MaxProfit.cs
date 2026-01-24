#!/usr/bin/env -S dotnet run

/*
https://leetcode.com/problems/best-time-to-buy-and-sell-stock/
*/

List<int> prices = [7, 10, 1, 3, 6, 9, 2];

Console.WriteLine(Solution.MaxProfit1(prices));

Console.WriteLine(Solution.MaxProfit2(prices));

internal static class Solution
{
    /*
    O(n^2) time complexity
    O(1) space complexity
    */
    internal static int MaxProfit1(List<int> prices)
    {
        int n = prices.Count;
        int result = 0;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                result = Math.Max(result, prices[j] - prices[i]);
            }
        }
        return result;
    }

    /*
    O(n) time complexity
    O(1) space complexity
    */
    internal static int MaxProfit2(List<int> prices)
    {
        int minSoFar = prices[0];
        int result = 0;

        for (int i = 0; i < prices.Count; i++)
        {
            minSoFar = Math.Min(minSoFar, prices[i]);
            result = Math.Max(result, prices[i] - minSoFar);
        }
        return result;
    }
}
