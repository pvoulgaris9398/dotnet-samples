using System.Globalization;

namespace Algorithms
{
    internal static class DayEleven
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
            WriteLine($"{nameof(DayEleven)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            var temp = (testing ? TestData : InputData).Blink(25).ToList();

            var result = temp.Count;

            WriteLine($"{nameof(result)}: {result}");

            var temp2 = (testing ? TestData : InputData).Blink2(25).Sum();

            var result2 = temp2;

            WriteLine($"{nameof(result2)}: {result2}");

        }

        private static IEnumerable<long> Blink2(this IEnumerable<long> stones, int times)
        {
            foreach (var stone in stones)
            {
                yield return stone.Count(times);
            }

            yield break;
        }

        private static long Count(this long stone, int times)
        {
            List<long> arrangement = [stone];
            Dictionary<(long, long), long> cache = [];



            foreach (var _ in Enumerable.Range(0, times))
            {
                arrangement = [.. NextArrangement(arrangement)];
            }
            cache.Add((stone, times), arrangement.Count);
            return arrangement.Count;
        }

        private static IEnumerable<long> Blink(this IEnumerable<long> stones, int times)
        {
            var arrangement = stones;
            foreach (var index in Enumerable.Range(0, times))
            {
                arrangement = [.. NextArrangement(arrangement)];
            }
            return arrangement;
        }

        private static IEnumerable<long> NextArrangement(this IEnumerable<long> stones)
            => stones.SelectMany(NextStone);

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
