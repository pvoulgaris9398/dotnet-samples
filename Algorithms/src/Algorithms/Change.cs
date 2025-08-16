namespace Algorithms
{
    internal static class Change
    {

        internal static void Test()
        {
            WriteLine($"{nameof(CountChangeTabulated)}: {CountChangeTabulated(10.00M, 0.01M, 0.05M, .10M, .25M, 0.50M, 1.00M):#,##0}");
            WriteLine($"{nameof(CountChangeMemoized)}: {CountChangeMemoized(10.00M, 0.01M, 0.05M, .10M, .25M, 0.50M, 1.00M):#,##0}");
        }

        private static readonly Dictionary<(decimal, int), int> CountCache = new() { { (0.00M, 0), 1 } };
        internal static int CountChangeMemoized(decimal amount, params decimal[] coins) =>
            CountCache.TryGetValue((amount, coins.Length), out int value) ? value
            : amount < 0 || coins.Length == 0 ? 0
            : CountCache[(amount, coins.Length)] = CountChangeMemoized(amount, coins[..^1]) + CountChangeMemoized(amount - coins[^1], coins);

        internal static int CountChangeTabulated(decimal amount, params decimal[] coins)
        {
            decimal unit = coins.Min();

            int[] counts = new int[(int)(amount / unit) + 1];

            counts[0] = 1;

            foreach (decimal coin in coins)
            {
                int step = (int)(coin / unit);
                for (int i = step; i < counts.Length; i++)
                {
                    counts[i] += counts[i - step];
                }
            }

            return counts[^1];
        }

        internal static double GetJointProbability(double[] eventProbabilities)
        {
            int k = eventProbabilities.Length;
            double[] p = new double[k + 1];
            p[0] = 1.0;

            foreach (var eventProbability in eventProbabilities)
            {
                for (int j = k; j > 0; j--)
                {
                    p[j] = (p[j] * (1 - eventProbability)) + (p[j - 1] * eventProbability);
                }
                p[0] *= 1 - eventProbability;
            }
            return p[^1];
        }
    }
}
