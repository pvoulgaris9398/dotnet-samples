using System.Globalization;
using CsvHelper;

#pragma warning disable IDE0160 // Convert to block scoped namespace
namespace BenchmarkResearch;

#pragma warning restore IDE0160 // Convert to block scoped namespace

public sealed class CsvLoader
{
    public void Process()
    {
        string fileName = @"C:\Users\Peter\_work\_tempdata\Crime_Data_from_2020_to_Present.csv";

        using var reader = new StreamReader(fileName);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        _ = csv.Read();
        _ = csv.ReadHeader();
        while (csv.Read())
        {
            Console.WriteLine(csv.GetField<string>("AREA"));
        }
    }
}
