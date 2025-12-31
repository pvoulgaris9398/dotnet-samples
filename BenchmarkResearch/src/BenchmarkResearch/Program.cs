using BenchmarkDotNet.Running;
using BenchmarkResearch;

if (args.Length > 0)
{
    Action actionToExecute = args[0] switch
    {
        "csv-loader" => Tests.TestCsvLoader,
        "string-loader" => Tests.TestStringLoader,
        "string-parser" => Tests.TestStringParsing,
        _ => () => Console.WriteLine("Unrecognized command-line argument"),
    };
    actionToExecute();
    return;
}

internal static class Tests
{
    public static void TestCsvLoader() => new CsvLoader().Process();

    public static void TestStringLoader() => BenchmarkRunner.Run<StringLoaderBenchmark>();

    public static void TestStringParsing() => BenchmarkRunner.Run<StringParsingBenchmark>();
}
