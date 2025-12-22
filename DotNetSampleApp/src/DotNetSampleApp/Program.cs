using static System.Console;

var now = DateTime.Now;

if (now > new DateTime(2022, 10, 1))
{
    WriteLine($"DotNetSampleApp is running {now}...");
}

var summary = BenchmarkRunner.Run<BenchmarkPerformance>();

WriteLine(summary);

WriteLine("Press any key to continue...");

var _ = Read();
