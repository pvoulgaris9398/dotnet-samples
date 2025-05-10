using System.Globalization;

namespace Algorithms
{
    internal static class Day11
    {
        private static IEnumerable<long> InputData => "890 0 1 935698 68001 3441397 7221 27"
            .Split(" ")
            .Select(long.Parse);

        private static IEnumerable<long> TestData => "125 17"
             .Split(" ")
            .Select(long.Parse);

        public static void Run(bool testing = false)
        {
            WriteLine(new string('*', 80));
            WriteLine($"{nameof(Day11)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            var result = (testing ? TestData : InputData).Count(25);

            WriteLine($"{nameof(result)}: {result:#,###}");

            var temp2 = (testing ? TestData : InputData).Count(75);

            var result2 = temp2;

            WriteLine($"{nameof(result2)}: {result2:#,###}");

        }

        private static readonly Dictionary<(long, long), long> Cache = [];

        private static long Count(this IEnumerable<long> stones, int times) =>
        stones.Sum(number => number.Count(times));

        private static long Count(this long stone, int times) =>
        Cache.TryGetValue((stone, times), out long count) ? count
        : Cache[(stone, times)] = stone.CountAll(times);

        private static long CountAll(this long number, int times) =>
            times == 0 ? 1 : Count(number.NextStone(), times - 1);

        private static IEnumerable<long> NextStone(this long input)
        {
            if (input == 0)
            {
                yield return 1L;
                yield break;
            }

            if (HasEvenDigits(input) && Parse(input) is (long, long) tuple1)
            {
                yield return tuple1.a;
                yield return tuple1.b;
                yield break;
            }
            yield return input * 2024;
        }

        private static (long a, long b)? Parse(this long input)
        {
            var temp = input.ToString(CultureInfo.InvariantCulture).ToCharArray();
            var temp2 = temp[..(temp.Length / 2)];
            var temp3 = temp[(temp.Length / 2)..];

            return (long.Parse(temp2, CultureInfo.InvariantCulture), long.Parse(temp3, CultureInfo.InvariantCulture));
        }

        private static bool HasEvenDigits(this long input) => input.ToString(CultureInfo.InvariantCulture).Length % 2 == 0;

    }
}
