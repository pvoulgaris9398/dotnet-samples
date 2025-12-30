using BenchmarkDotNet.Running;
using BenchmarkResearch;

if (args.Length > 0)
{
    Action actionToExecute = args[0] switch
    {
        "test0" => Tests.Test0,
        "test1" => Tests.Test1,
        _ => () => Console.WriteLine("Unrecognized command-line argument"),
    };
    actionToExecute();
    return;
}

internal static class Tests
{
    public static void Test0()
    {
        Console.WriteLine("Test0: CsvLoader Process");
        new CsvLoader().Process();
    }

    public static void Test1()
    {
        BenchmarkRunner.Run<StringLoaderBenchmark>();
        Console.WriteLine("Test1: ");
    }

}
