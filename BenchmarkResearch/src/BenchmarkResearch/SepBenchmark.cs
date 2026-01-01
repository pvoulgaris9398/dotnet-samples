using nietras.SeparatedValues;

#pragma warning disable IDE0160 // Convert to block scoped namespace
namespace BenchmarkResearch;

#pragma warning restore IDE0160 // Convert to block scoped namespace
public sealed class SepBenchmark
{
    public void Process()
    {
        string fileName = @"C:\Users\Peter\_work\_tempdata\Crime_Data_from_2020_to_Present.csv";

        using var reader = Sep.Reader().FromFile(fileName);

        foreach (var row in reader)
        {
            Console.WriteLine(row["AREA"].Span);
        }
    }
}
