namespace Algorithms
{
    internal static class Fibonacci
    {
        private static Dictionary<int, int> cache = new() { { 0, 0 }, { 1, 1 } };


        /*  Memoized version
         */
        internal static int fibMemoized(int n) =>
            cache.TryGetValue(n, out int value) ? value
            : cache[n] = fibMemoized(n - 1) + fibMemoized(n - 2);

        /*  Dynamic Programming with tabulation
         */
        internal static int fibTabulated(int n)
        {
            int[] fib = new int[n + 1];
            fib[1] = 1;

            for (int i = 2; i <= n; i++)
            {
                fib[i] = fib[i - 1] + fib[i - 2];
            }

            return fib[^1];
        }

        /* Dynamic Programming with sliding window
         */
        internal static int fib1(int n)
        {
            if (n < 1) return 0;
            if (n < 2) return 1;

            int a = 0;
            int b = 1;

            for (int i = 2; i <= n; i++)
            {
                (a, b) = (b, a + b);
            }

            return b;
        }
    }
}
