#!/usr/bin/env -S dotnet run

Console.WriteLine(new Solution().MaxBottlesDrunk(13, 6));

Console.WriteLine(new Solution().MaxBottlesDrunk(10, 3));

/*
https://leetcode.com/problems/water-bottles-ii/
*/
public class Solution
{
    public int MaxBottlesDrunk(int numBottles, int numExchange)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(numBottles, 1, nameof(numBottles));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(numBottles, 100, nameof(numBottles));
        ArgumentOutOfRangeException.ThrowIfLessThan(numExchange, 1, nameof(numExchange));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(numExchange, 100, nameof(numExchange));

        int full = numBottles;
        int exchange_min = numExchange;
        int empty =0;
        int drunk = 0;

        while (full >= 0)
        {
            full--;
            drunk++;
            empty++;
            if (full == 0)
            {
                if (empty < exchange_min)
                    return drunk;

                while (empty >= exchange_min)
                {
                    empty = empty - exchange_min;
                    full++;
                    exchange_min++;
                }
            }
        }
        return drunk;
    }
}
