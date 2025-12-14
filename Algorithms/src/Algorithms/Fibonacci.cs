namespace Algorithms
{
    internal static class Fibonacci
    {
        private static readonly Dictionary<int, int> Cache = new() { { 0, 0 }, { 1, 1 } };


        /*  Memoized version
         */
        internal static int FibMemoized(int n) =>
            Cache.TryGetValue(n, out int value) ? value
            : Cache[n] = FibMemoized(n - 1) + FibMemoized(n - 2);

        /*  Dynamic Programming with tabulation
         */
        internal static int FibTabulated(int n)
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
        internal static int Fib1(int n)
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
