using System.Globalization;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using CsvHelper;
using CsvHelper.Configuration;
using nietras.SeparatedValues;

#pragma warning disable IDE0160 // Convert to block scoped namespace
namespace BenchmarkResearch;

#pragma warning restore IDE0160 // Convert to block scoped namespace

[MemoryDiagnoser]
public class CsvBenchmark
{
    private static string FileName => @"C:\Users\Peter\_work\_tempdata\stocks\all_stocks_5yr.csv";

    private readonly Consumer _consumer = new();

    [Params(1, 100, 1_000, 10_000, 50_000, 100_000, 150_000, 500_000)]
    public int Rows { get; set; }

    [Benchmark]
    public void RunTestCsvHelper() => TestCsvHelper().Consume(_consumer);

    [Benchmark]
    public void RunTestSep() => TestSep().Consume(_consumer);

    internal IEnumerable<BenchmarkDataRow> TestCsvHelper()
    {
        CsvConfiguration config = new(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = true,
            BadDataFound = arg => Console.WriteLine($"Bad data found: {arg}!"),
        };

        /*
        EnumerableTextReader with file contents as IEnumerable<string> or below:
        */

        using var reader = File.OpenText(FileName);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        _ = csv.Read();
        _ = csv.ReadHeader();

        while (csv.Read())
        {
            /*
            Double-check any overloads here that take an integer (column index) or
            a string (column header) vs.
            those that are generic vs.
            those that use ReadOnlySpan<char> (if available)
            Want to see if we can reduce memory usage at all here:
            https://github.com/JoshClose/CsvHelper
            */
            yield return new(
                csv.GetField("date") ?? "",
                csv.GetField("close") ?? "",
                csv.GetField("Name") ?? ""
            );
        }
    }

    internal IEnumerable<BenchmarkDataRow> TestSep()
    {
        using var reader = Sep.Reader().FromFile(FileName);

        foreach (var row in reader)
        {
            yield return new(
                row["date"].ToString(),
                row["close"].ToString(),
                row["Name"].ToString()
            );
        }
    }
}
