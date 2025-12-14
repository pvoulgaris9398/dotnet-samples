namespace Algorithms.ProjectEuler;

internal static class Puzzle001
{
    public static void Run()
    {
        int sum1 = MultiplesOf(999, [3, 5]).Sum();

        WriteLine($"{sum1}");

        int sum2 = MultiplesOf(999).Sum();

        WriteLine($"{sum2}");

    }

    private static IEnumerable<int> MultiplesOf(int input, int[] numbers) => Enumerable.Range(1, input)
        .Where(i => numbers.Any(n => i % n == 0));

    private static IEnumerable<int> MultiplesOf(int input) => Enumerable.Range(1, input)
        .Where(i => i.IsMultipleOf());

    private static bool IsMultipleOf(this int source) => source % 3 == 0 || source % 5 == 0;

}
