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

internal static class MeanReversionStrategy
{
    internal static void AwaitMarketOpen() => Console.WriteLine($"{nameof(AwaitMarketOpen)}");

    internal static void SubmitOrder(int quantity, decimal price, string side) =>
        Console.WriteLine(
            $"{nameof(SubmitOrder)}: Quantity: {quantity}\tPrice: {price}\tSide: {side}"
        );

    internal static void ClosePosition() => Console.WriteLine($"{nameof(ClosePosition)}");

    /*
    Start? Initialize?
    (1) Get list of all current orders
    (2) Delete them as a fresh start
    (3) Get exchange info, trading calendar, open and close times, etc.
    */
    internal static void Start() => Console.WriteLine($"{nameof(Start)}");

    /*
    (1) Get current account info, buying power, portfolio value, etc.
    Buying Power: Settled cash and any available margin funds
    (2) Get current position info, quantity, current price, etc.
    */
    internal static void Run() => Console.WriteLine($"{nameof(Run)}");

    /*
    (0) x = 20, scale = 200
    (1) Get last x minutes worth of prices for ticker or symbol
    (2) Calculate average price over that period
    (3) Get current (latest) price
    (3) Calculate average - current = diff
    (4) If diff < 0 or current price greater than last x minute average
        - Take profit (sell any open positions)
        - Signal => SELL
    (5) If diff > 0 or current price less than last x minute average
        - Calculate amount to add to portfolio
            - Calculate portfolioShare = diff/currentPrice*scale
            - Calculate targetPositionValue = portfolioValue*portfolioShare
            - Calculate amountToAdd = targetPositionValue - positionValue
        - If amount to add > 0 then BUY as much as possible, taking into account buying power => BUY
        (Assuming this works for only one ticker/symbol)
        - If amount to add < 0 then SELL up to "amount to add"
                
    */
    internal static void CalculateSignal(string ticker) =>
        Console.WriteLine($"{nameof(CalculateSignal)}: Ticker: {ticker}");

    internal static decimal CalculatePortfolioShare(
        decimal twentyMinuteAverage,
        decimal currentPrice,
        decimal scale = 1
    ) => (twentyMinuteAverage - currentPrice) / currentPrice * scale;

    internal static decimal CalculateTargetPositionValue(
        decimal portfolioValue,
        decimal portfolioShare
    ) => portfolioValue * portfolioShare;

    internal static decimal CalculateAmountToAdd(
        decimal targetPositionValue,
        decimal positionValue
    ) => targetPositionValue - positionValue;
}

internal static class ExponentialMovingAverage
{
    internal static IEnumerable<double> Calculate(IEnumerable<double> data, int windowSize)
    {
        int counter = 0;

        IEnumerator<double> enumerator = data.GetEnumerator();
        double currentSum = 0;
        double k = 2.0 / (windowSize + 1);
        double ema = 0;

        while (enumerator.MoveNext())
        {
            /*
            EMA = Price(t) * k + EMA(t-1)*(1-k)
            at t = 0 there is not price from yesterday so
            EMA starts off as previous average
            */
            currentSum += enumerator.Current;
            counter += 1;
            ema = currentSum / counter;
            if (counter == windowSize)
                break;
        }

        while (enumerator.MoveNext())
        {
            ema = (enumerator.Current * k) + (ema * (1 - k));
            yield return ema;
        }
        yield break;
    }
}

internal static class SimpleMovingAverage
{
    internal static IEnumerable<double> Calculate(IEnumerable<double> data, int windowSize)
    {
        Queue<double> window = [];

        IEnumerator<double> enumerator = data.GetEnumerator();

        double currentSum = 0;
        while (enumerator.MoveNext())
        {
            double currentPrice = enumerator.Current;
            currentSum += currentPrice;
            window.Enqueue(currentPrice);
            if (window.Count > windowSize)
            {
                currentSum -= window.Dequeue();
            }
            if (window.Count == windowSize)
            {
                yield return currentSum / windowSize;
            }
        }
        yield break;
    }
}

internal static class Tests
{
    internal static void Test0()
    {
        MeanReversionStrategy.Start();
        MeanReversionStrategy.Run();
        MeanReversionStrategy.AwaitMarketOpen();
        MeanReversionStrategy.CalculateSignal("IBM");
        MeanReversionStrategy.CalculateSignal("AAPL");
        MeanReversionStrategy.CalculateSignal("EV");
        MeanReversionStrategy.CalculateSignal("GOOG");
        decimal twentyMinuteAverage = 90;
        decimal currentPrice = 85;
        decimal scale = 200;
        var portfolioShare = MeanReversionStrategy.CalculatePortfolioShare(
            twentyMinuteAverage,
            currentPrice,
            scale
        );
        var portfolioValue = 274_000;
        var positionValue = 0;
        var targetPositionValue = MeanReversionStrategy.CalculateTargetPositionValue(
            portfolioValue,
            portfolioShare
        );
        Console.WriteLine(
            MeanReversionStrategy.CalculateAmountToAdd(targetPositionValue, positionValue)
        );
        MeanReversionStrategy.SubmitOrder(101, 125.75m, "Buy");
        MeanReversionStrategy.ClosePosition();
    }

    internal static void Test1()
    {
        Console.WriteLine($"{nameof(SimpleMovingAverage)}");
        Random random = new();
        double min = 50;
        double max = 100;
        int windowSize = 4;
        int numberOfItemsInSequence = 10;

        Func<int, double> generationStrategy1 = i => (random.NextDouble() * (max - min)) + min;

        Func<int, double> generationStrategy2 = i => i;

        var prices = Enumerable
            .Range(1, numberOfItemsInSequence)
            .Select(i => generationStrategy2(i));
        var results = SimpleMovingAverage.Calculate(prices, windowSize);
        foreach (var (value, index) in results.Select((value, index) => (value, index)))
        {
            Console.WriteLine($"Item: {index}\tValue: {value}");
        }
    }

    internal static void Test2()
    {
        Console.WriteLine($"{nameof(ExponentialMovingAverage)}");
        Random random = new();
        double min = 50;
        double max = 100;
        int windowSize = 4;
        int numberOfItemsInSequence = 10;

        Func<int, double> generationStrategy1 = i => (random.NextDouble() * (max - min)) + min;

        Func<int, double> generationStrategy2 = i => i;

        var prices = Enumerable
            .Range(1, numberOfItemsInSequence)
            .Select(i => generationStrategy2(i));
        var results = ExponentialMovingAverage.Calculate(prices, windowSize);
        foreach (var (value, index) in results.Select((value, index) => (value, index)))
        {
            Console.WriteLine($"Item: {index}\tValue: {value}");
        }
    }
}
