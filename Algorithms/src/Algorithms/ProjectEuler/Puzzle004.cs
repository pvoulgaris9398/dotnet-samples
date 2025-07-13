namespace Algorithms.ProjectEuler
{
    internal static class Puzzle004
    {

        internal sealed record Result(int Factor1, int Factor2, int Product);

        internal static void Run()
        {
            WriteLine($"{nameof(Puzzle004)}");

            // C# records do not implement IComparable
            WriteLine(LargestPalindromic(numberOfDigits: 2).Max(x => x.Product));
            WriteLine(LargestPalindromic(numberOfDigits: 3).Max(x => x.Product));

            // Interesting
            WriteLine((int.MaxValue).ToFactors());
            WriteLine((0).ToFactors());
            WriteLine((-445).ToFactors());

            WriteLine(IsPalindromic(6570756));
            WriteLine(IsPalindromic(65712756));

        }

        internal static IEnumerable<Result> LargestPalindromic(int numberOfDigits)
        {
            // Sanity Check
            if (numberOfDigits < 0 || numberOfDigits > 5)
            {
                yield break;
            }

            var (start, stop) = ToFactors(numberOfDigits);

            for (var first = start; first >= stop; first--)
                for (var second = start; second >= stop; second--)
                {
                    var product = first * second;
                    if (product.IsPalindromic())
                    {
                        yield return new(first, second, product);
                    }
                }
        }

        internal static (int, int) ToFactors(this int numberOfDigits) => ((int)Math.Pow(10, numberOfDigits) - 1, (int)Math.Pow(10, numberOfDigits - 1));

        internal static bool IsPalindromic(this int number)
        {
            var digits = number.ToDigits().ToArray();
            var li = 0;
            var ri = digits.Length - 1;
            while (li < ri)
            {
                var first = digits[li];
                var last = digits[ri];

                if (first != last)
                {
                    return false;
                }
                li++;
                ri--;
            }
            return true;
        }

        private static IEnumerable<int> ToDigits(this int number)
        {
            while (number > 0)
            {
                yield return number % 10;
                number = number / 10;
            }
        }
    }
}
