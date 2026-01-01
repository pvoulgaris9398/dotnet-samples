using System.Data;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

#pragma warning disable IDE0160 // Convert to block scoped namespace
namespace BenchmarkResearch;

#pragma warning restore IDE0160 // Convert to block scoped namespace

private static string FileName => @"C:\Users\Peter\_work\_tempdata\stocks\all_stocks_5yr.csv";

public sealed class CsvBenchmark
{
    [Benchmark]
    public void TestCsvHelper()
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

        using var reader = new StreamReader(fileName);
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
            yield return new(csv.GetField("date"), csv.GetField("close"), csv.GetField("Name"));
        }
    }

    [Benchmark]
    public IEnumerable<DataRow> TestSep()
    {
        using var reader = Sep.Reader().FromFile(fileName);

        foreach (var row in reader)
        {
            yield return new(row["date"].Span, row["close"].Span, row["Name"].Span);
        }
    }
}
