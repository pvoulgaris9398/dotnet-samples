#!/usr/bin/env -S dotnet run

Console.WriteLine(MaxProfit([2, 7, 11, 15]));

static int MaxProfit(int[] prices)
{
    int minSoFar = prices[0];
    int maxSoFar = int.MinValue;
    List<int> profits = [0];

    foreach (var price in prices.Skip(1))
    {
        if (price < minSoFar)
        {
            profits.Add(maxSoFar - minSoFar);
            minSoFar = price;
        }
        if (price > maxSoFar)
        {
            profits.Add(maxSoFar - minSoFar);
            maxSoFar = price;
        }
    }
    return profits.Max();
}
