using BenchmarkDotNet.Running;
using BenchmarkResearch;

if (args.Length > 0)
{
    Action actionToExecute = args[0] switch
    {
        "csv-benchmark" => Tests.RunCsvBenchmark,
        "string-loader" => Tests.TestStringLoader,
        "string-parser" => Tests.TestStringParsing,
        "sep-benchmark" => Tests.TestSep,
        _ => () => Console.WriteLine("Unrecognized command-line argument"),
    };
    actionToExecute();
    return;
}

internal static class Tests
{
    public static void RunCsvBenchmark() => BenchmarkRunner.Run<CsvBenchmark>();

    public static void TestStringLoader() => BenchmarkRunner.Run<StringLoaderBenchmark>();

    public static void TestStringParsing() => BenchmarkRunner.Run<StringParsingBenchmark>();

    public static void TestSep() => new SepBenchmark().Process();
}
