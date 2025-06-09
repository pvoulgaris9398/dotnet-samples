using System.Collections.Concurrent;
using System.Diagnostics;

namespace Algorithms.ProjectEuler
{
    internal class Puzzle002
    {
        public static void Run()
        {
            var f = new Fibonacci();
            var sum = 0L;
            var i = -1;

            while (true)
            {
                i++;
                var temp = f.Calculate1(i);
                var temp2 = f.Calculate3(i);
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
                var result = f.Calculate1(i);
                if (logIntermediate) WriteLine($"{result}");
            }

            stopWatch.Stop();

            WriteLine($"Ellapsed time for {nameof(Fibonacci.Calculate1)} was {stopWatch.Elapsed}");
        }

        public async static Task Test2(int number, bool logIntermediate = false)
        {
            var f = new Fibonacci();

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (long i in Enumerable.Range(0, number))
            {
                var result = await f.Calculate2(i);
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
                var result1 = f.Calculate1(i);
                var result3 = f.Calculate3(i);
                if (logIntermediate) WriteLine($"Iteration # {i}:\t{nameof(result1)}: {result1}\t{nameof(result3)}: {result3}");
            }

            stopWatch.Stop();

            WriteLine($"Ellapsed time for {nameof(Fibonacci.Calculate2)} was {stopWatch.Elapsed}");
        }

        private class Fibonacci
        {
            private ConcurrentDictionary<long, long> _cache = new()
            {
                [0] = 0,
                [1] = 1
            };

            internal long Calculate1(long number)
            {
                if (_cache.ContainsKey(number)) return _cache[number];

                return _cache[number] = Calculate1(number - 1) + Calculate1(number - 2);
            }

            /// <summary>
            /// Actually, slower than Calculate1
            /// </summary>
            /// <param name="number"></param>
            /// <returns></returns>
            internal async Task<long> Calculate2(long number)
            {
                if (_cache.ContainsKey(number)) return _cache[number];

                var task1 = Task.Run(() => Calculate2(number - 1));
                var task2 = Task.Run(() => Calculate2(number - 2));

                await Task.WhenAll(task1, task2);

                return _cache[number] = task1.Result + task2.Result;
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
            internal long Calculate3(long number)
            {
                var PHI = ((1 + Math.Sqrt(5)) / 2);
                var PSI = ((1 - Math.Sqrt(5)) / 2);
                var numerator = Math.Pow(PHI, number) - Math.Pow(PSI, number);
                var denominator = Math.Sqrt(5);

                var result = numerator / denominator;

                return (long)result;
            }
        }
    }
}
