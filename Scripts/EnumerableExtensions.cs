#!/usr/bin/env -S dotnet

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

internal static class EnumerableExtensions
{
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
