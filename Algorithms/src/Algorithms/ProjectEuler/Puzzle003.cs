namespace Algorithms.ProjectEuler
{
    internal static class Puzzle003
    {
        internal static void Run()
        {
            int n = 100;
            Func<int, long> square = n => n * n;

            var sumOfSquares = Enumerable.Range(1, 100)
                .Select(square)
                .Sum();

            var squareOfSum = square(Enumerable.Range(1, 100)
                .Sum());

            var difference = squareOfSum - sumOfSquares;

            WriteLine($"{nameof(sumOfSquares)} of first {n} Natural Numbers is: {sumOfSquares}");
            WriteLine($"{nameof(squareOfSum)} of first {n} Natural Numbers is: {squareOfSum}");
            WriteLine($"{nameof(difference)} is {difference}");
        }
    }
}
