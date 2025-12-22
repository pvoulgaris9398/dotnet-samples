#!/usr/bin/env -S dotnet

if (args.Length > 0)
{
    Action actionToExecute = args[0] switch
    {
        "test0" => Tests.Test0,
        "test1" => Tests.Test1,
        "test2" => Tests.Test2,
        _ => () => Console.WriteLine("Unrecognized command-line argument!"),
    };
    actionToExecute();
    return;
}

internal static class Tests
{
    internal static void Test0()
    {
        int[] input = [1, 2, 3, 4, 5, 6, 7];

        Console.WriteLine(new string('*', 80));
        input.Print();
        Console.WriteLine();

        Console.WriteLine(new string('*', 80));
        Console.WriteLine("Pairs:");
        input.TakePairs().Print();
        Console.WriteLine();

        Console.WriteLine(new string('*', 80));
        Console.WriteLine("Chained Pairs:");
        input.GetTakeChainedPairs().Print();
        Console.WriteLine();
    }

    internal static void Test1()
    {
        char[,] testData =
        {
            { '6', '4', ' ' },
            { '2', '3', ' ' },
            { '3', '1', '4' },
        };

        Console.WriteLine(new string('*', 80));
        Console.WriteLine("Original Array:");
        testData.Print();
        Console.WriteLine();

        var transposed = testData.Transpose();
        Console.WriteLine(new string('*', 80));
        Console.WriteLine("Transposed Array:");
        transposed.Print();
        Console.WriteLine();
    }

    internal static void Test2()
    {
        char[,] testData =
        {
            { ' ', '5', '1' },
            { '3', '8', '7' },
            { '2', '1', '5' },
        };

        Console.WriteLine(new string('*', 80));
        Console.WriteLine("Original Array:");
        testData.Print();
        Console.WriteLine();

        var transposed = testData.Transpose();
        Console.WriteLine(new string('*', 80));
        Console.WriteLine("Transposed Array:");
        transposed.Print();
        Console.WriteLine();
    }
}

internal static class EnumerableExtensions
{
    public static T[,] Transpose<T>(this T[,] source)
    {
        int rows = source.GetLength(0);
        int columns = source.GetLength(1);

        T[,] result = new T[columns, rows];

        for (int columnIndex = 0; columnIndex < columns; columnIndex++)
        {
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                result[columnIndex, rowIndex] = source[rowIndex, columnIndex];
            }
        }
        return result;
    }

    public static void Print<T>(this T[,] source)
    {
        foreach (int rowIndex in Enumerable.Range(0, source.GetLength(0)))
        {
            foreach (int columnIndex in Enumerable.Range(0, source.GetLength(1)))
            {
                Console.Write($"{source[rowIndex, columnIndex]}");
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }

    extension<T>(IEnumerable<T> source)
    {
        public void Print()
        {
            foreach (T item in source)
            {
                Console.WriteLine($"{typeof(T)}: {item}");
            }
        }

        public IEnumerable<(T first, T second)> GetTakeChainedPairs()
        {
            if (source is null)
            {
                yield break;
            }

            using var enumerator = source.GetEnumerator();

            if (enumerator.MoveNext())
            {
                var first = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    var second = enumerator.Current;
                    yield return (first, second);
                    first = second;
                }
            }
            yield break;
        }

        public IEnumerable<(T first, T second)> TakePairs()
        {
            if (source is null)
            {
                yield break;
            }
            using var enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var first = enumerator.Current;
                if (enumerator.MoveNext())
                {
                    var second = enumerator.Current;
                    yield return (first, second);
                }
            }
            yield break;
        }
    }
}
