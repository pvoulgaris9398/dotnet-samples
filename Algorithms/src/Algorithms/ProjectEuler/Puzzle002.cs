using System.Collections.Concurrent;
using System.Diagnostics;

namespace Algorithms.ProjectEuler
{
#pragma warning disable CA1812
    internal sealed class Puzzle002
#pragma warning restore CA1812
    {
        public static void Run()
        {
            var f = new Fibonacci();
            long sum = 0L;
            int i = -1;

            while (true)
            {
                i++;
                long temp = f.Calculate1(i);
#pragma warning disable IDE0059
#pragma warning disable S1481
                long temp2 = Fibonacci.Calculate3(i);
#pragma warning restore S1481
#pragma warning restore IDE0059
                if (temp >= 4000000) break;
                if (temp % 2 == 0) sum += temp;
            }

            WriteLine($"Result: {sum}");
        }

        public static void Test1(int number, bool logIntermediate = false)
        {
            var f = new Fibonacci();
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (long i in Enumerable.Range(0, number))
            {
                long result = f.Calculate1(i);
                if (logIntermediate) WriteLine($"{result}");
            }

            stopWatch.Stop();

            WriteLine($"Ellapsed time for {nameof(Fibonacci.Calculate1)} was {stopWatch.Elapsed}");
        }

        public static async Task Test2(int number, bool logIntermediate = false)
        {
            var f = new Fibonacci();

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (long i in Enumerable.Range(0, number))
            {
                long result = await f.Calculate2(i).ConfigureAwait(false);
                if (logIntermediate) WriteLine($"{result}");
            }

            stopWatch.Stop();

            WriteLine($"Ellapsed time for {nameof(Fibonacci.Calculate2)} was {stopWatch.Elapsed}");
        }

        public static void Test3(int number, bool logIntermediate = false)
        {
            var f = new Fibonacci();

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (long i in Enumerable.Range(0, number))
            {
                long result1 = f.Calculate1(i);
                long result3 = Fibonacci.Calculate3(i);
                if (logIntermediate) WriteLine($"Iteration # {i}:\t{nameof(result1)}: {result1}\t{nameof(result3)}: {result3}");
            }

            stopWatch.Stop();

            WriteLine($"Ellapsed time for {nameof(Fibonacci.Calculate2)} was {stopWatch.Elapsed}");
        }

        private sealed class Fibonacci
        {
            private readonly ConcurrentDictionary<long, long> _cache = new()
            {
                [0] = 0,
                [1] = 1
            };

            internal long Calculate1(long number) => _cache.TryGetValue(number, out long value) ? value : (_cache[number] = Calculate1(number - 1) + Calculate1(number - 2));

            /// <summary>
            /// Actually, slower than Calculate1
            /// </summary>
            /// <param name="number"></param>
            /// <returns></returns>
            internal async Task<long> Calculate2(long number)
            {
                if (_cache.TryGetValue(number, out long value)) return value;

                Task<long> task1 = Task.Run(() => Calculate2(number - 1));
                Task<long> task2 = Task.Run(() => Calculate2(number - 2));

                _ = await Task.WhenAll(task1, task2).ConfigureAwait(false);
#pragma warning disable CA1849
                return _cache[number] = task1.Result + task2.Result;
#pragma warning restore CA1849
            }

            /// <summary>
            /// Resources:
            /// https://en.wikipedia.org/wiki/Golden_ratio
            /// https://mathworld.wolfram.com/GoldenRatio.html
            /// https://mathworld.wolfram.com/BinetsFormula.html
            /// Between number 71 and 72, Binet's Formula is inaccurate
            /// See: https://www.geeksforgeeks.org/find-nth-fibonacci-number-using-binets-formula/
            /// TODO: Figure out what can be done about this?
            /// </summary>
            /// <param name="number"></param>
            /// <returns></returns>
            internal static long Calculate3(long number)
            {
                double PHI = (1 + Math.Sqrt(5)) / 2;
                double PSI = (1 - Math.Sqrt(5)) / 2;
                double numerator = Math.Pow(PHI, number) - Math.Pow(PSI, number);
                double denominator = Math.Sqrt(5);

                double result = numerator / denominator;

                return (long)result;
            }
        }
    }
}
