namespace Algorithms.ProjectEuler
{
    internal static class Puzzle006
    {
        internal static void Run()
        {
            int n = 100;
            static long square(int n)
            {
                return n * n;
            }

            long sumOfSquares = Enumerable.Range(1, 100)
                .Sum(square);

            long squareOfSum = square(Enumerable.Range(1, 100)
                .Sum());

            long difference = squareOfSum - sumOfSquares;

            WriteLine($"{nameof(sumOfSquares)} of first {n} Natural Numbers is: {sumOfSquares}");
            WriteLine($"{nameof(squareOfSum)} of first {n} Natural Numbers is: {squareOfSum}");
            WriteLine($"{nameof(difference)} is {difference}");
        }
    }
}
