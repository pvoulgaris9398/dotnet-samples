namespace Algorithms;

public static class ListExtensions
{
    public static void Print(List<List<int>> input)
    {
        foreach (List<int> row in input)
        {
            foreach (int column in row)
            {
                Console.Write($"{column} ");
            }
            Console.WriteLine();
        }
    }
}
