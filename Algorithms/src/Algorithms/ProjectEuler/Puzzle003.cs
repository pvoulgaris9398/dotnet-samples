namespace Algorithms.ProjectEuler
{
    internal static class Puzzle003
    {
        internal static void Run()
        {
            var number = 13195L;
            //var number = 600851475143;
            var factors = number.Factors().ToList();

            foreach (var (index, factor) in factors.Select((f, i) => (i + 1, f)))
            {
                WriteLine($"Factor Number: {index} of {number} is {factor}");
            }

            WriteLine($"Largest Prime Factor of: {number} is {factors.Max()}");

        }

        public static void GeneratePrimes(int upTo)
        {
            var primes = Enumerable.Range(1, upTo).Where(IsPrime);

            foreach (var prime in primes)
            {
                WriteLine(prime);
            }

        }

        public static IEnumerable<long> Factors(this long input)
        {
            var temp = input;
            var currentPrime = 2L;

            while (true)
            {
                var (success, dividend) = temp.AttemptDivide(currentPrime);
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
            var temp = number + 1;
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

            var boundary = (long)Math.Floor(Math.Sqrt(number));

            // https://en.wikipedia.org/wiki/Prime_number
            for (long i = 3L; i <= boundary; i += 2L)
            {
                if (number % i == 0) return false;
            }
            return true;

        }
    }
}
