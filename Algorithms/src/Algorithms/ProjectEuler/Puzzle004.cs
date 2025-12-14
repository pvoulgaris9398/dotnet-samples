namespace Algorithms.ProjectEuler;

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
        WriteLine(int.MaxValue.ToFactors());
        WriteLine(0.ToFactors());
        WriteLine((-445).ToFactors());

        WriteLine(IsPalindromic(6570756));
        WriteLine(IsPalindromic(65712756));

    }

    internal static IEnumerable<Result> LargestPalindromic(int numberOfDigits)
    {
        // Sanity Check
        if (numberOfDigits is < 0 or > 5)
        {
            yield break;
        }

        (int start, int stop) = ToFactors(numberOfDigits);

        for (int first = start; first >= stop; first--)
        {
            for (int second = start; second >= stop; second--)
            {
                int product = first * second;
                if (product.IsPalindromic())
                {
                    yield return new(first, second, product);
                }
            }
        }
    }

    internal static (int, int) ToFactors(this int numberOfDigits) => ((int)Math.Pow(10, numberOfDigits) - 1, (int)Math.Pow(10, numberOfDigits - 1));

    internal static bool IsPalindromic(this int number)
    {
        int[] digits = [.. number.ToDigits()];
        int li = 0;
        int ri = digits.Length - 1;
        while (li < ri)
        {
            int first = digits[li];
            int last = digits[ri];

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
            number /= 10;
        }
    }
}
