namespace Algorithms
{
    internal static class Change
    {
        private static Dictionary<(decimal, int), int> countCache = new() { { (0.00M, 0), 1 } };
        internal static int countChangeMemoized(decimal amount, params decimal[] coins) =>
    countCache.TryGetValue((amount, coins.Length), out int value) ? value
    : amount < 0 || coins.Length == 0 ? 0
    : countCache[(amount, coins.Length)] = countChangeMemoized(amount, coins[..^1]) + countChangeMemoized(amount - coins[^1], coins);

        internal static int countChangeTabulated(decimal amount, params decimal[] coins)
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

        internal static double getJointProbability(double[] eventProbabilities, int k)
        {
            double[] p = new double[k + 1];
            p[0] = 1.0;

            foreach (var eventProbability in eventProbabilities)
            {
                for (int j = k; j > 0; j--)
                {
                    p[j] = p[j] * (1 - eventProbability) + p[j - 1] * eventProbability;
                }
                p[0] *= 1 - eventProbability;
            }
            return p[^1];
        }
    }
}
