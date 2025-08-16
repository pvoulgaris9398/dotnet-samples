namespace Algorithms
{
    internal static class SieveOfErastothenes
    {


        public static IEnumerable<int> FindCandidatePrimes2(int limit)
        {
            // Nope! :-)
            return Enumerable.Range(2, limit)
                .Where(i => i % 2 == 0)
                .Where(i => i % 3 == 0);
        }

        public static IEnumerable<int> FindCandidatePrimes(int limit)
        {
            bool[] isPrime = [.. Enumerable.Repeat(true, limit + 1)];

            for (int p = 2; p * p <= limit; p++)
            {
                if (isPrime[p])
                {
                    for (int i = p * p; i <= limit; i += p)
                    {
                        isPrime[i] = false;
                    }
                }

            }
            for (int i = 2; i <= limit; i++)
            {
                if (isPrime[i])
                {
                    yield return i;
                }
            }
        }

        internal static void Run(int limit)
        {
            WriteLine($"Prime numbers up to {limit}:");
            foreach (int prime in FindCandidatePrimes(limit))
            {
                WriteLine(prime);
            }
            WriteLine();
        }

    }
}