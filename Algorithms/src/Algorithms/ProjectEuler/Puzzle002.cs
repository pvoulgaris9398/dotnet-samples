namespace Algorithms.ProjectEuler
{
    internal static class Puzzle002
    {
        public static void Run()
        {
            var n = 10;
            var result = Enumerable.Range(0, n).Select(i => Calculate(i)).ToList();
            foreach (var num in result)
            {
                WriteLine($"{num}");
            }

            WriteLine(new string('*', 80));

            var result2 = Generate(n).ToList();
            foreach (var num2 in result2)
            {
                WriteLine($"{num2}");
            }

        }

        public static IEnumerable<int> Generate(int n)
        {
            var prev = 0;
            var nxt = 1;

            if (n < 2)
            {
                yield return n;
            }

            for (int i = 2; i < n; i++)
            {
                prev = nxt;
                nxt = prev + nxt;
            }
            yield return nxt;
        }

        public static int Calculate2(int n)
        {
            var prev = 0;
            var nxt = 1;

            if (n < 2)
            {
                return n;
            }

            for (int i = 2; i < n; i++)
            {
                prev = nxt;
                nxt = prev + nxt;
            }
            return nxt;
        }

        public static int Calculate(int n)
        {
            if (n < 2)
                return n;
            else
                return Calculate(n - 1) + Calculate(n - 2);
        }
    }
}