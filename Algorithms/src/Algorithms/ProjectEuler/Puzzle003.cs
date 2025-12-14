namespace Algorithms.ProjectEuler;

internal static class Puzzle003
{
    internal static void Run()
    {
        long number = 13195L;
        //var number = 600851475143;
        var factors = number.Factors().ToList();

        foreach ((int index, long factor) in factors.Select((f, i) => (i + 1, f)))
        {
            WriteLine($"Factor Number: {index} of {number} is {factor}");
        }

        WriteLine($"Largest Prime Factor of: {number} is {factors.Max()}");

    }

    public static void GeneratePrimes(int upTo)
    {
        IEnumerable<int> primes = Enumerable.Range(1, upTo).Where(IsPrime);

        foreach (int prime in primes)
        {
            WriteLine(prime);
        }

    }

    public static IEnumerable<long> Factors(this long input)
    {
        long temp = input;
        long currentPrime = 2L;

        while (true)
        {
            (bool success, long dividend) = temp.AttemptDivide(currentPrime);
            if (success)
            {
                yield return currentPrime;
                temp = dividend;
            }
            else
            {
                currentPrime = currentPrime.NextPrime();
            }
            if (dividend == 1) break;
        }
    }

    public static (bool, long) AttemptDivide(this long number, long divisor)
    {
        if (number % divisor == 0)
        {
            return (true, number / divisor);
        }
        return (false, number);
    }

    public static long NextPrime(this long number)
    {
        long temp = number + 1;
        while (!temp.IsPrime())
        {
            temp++;
        }
        return temp;
    }

    public static bool IsPrime(this int number) => ((long)number).IsPrime();

    public static bool IsPrime(this long number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        long boundary = (long)Math.Floor(Math.Sqrt(number));

        // https://en.wikipedia.org/wiki/Prime_number
        for (long i = 3L; i <= boundary; i += 2L)
        {
            if (number % i == 0) return false;
        }
        return true;

    }
}
