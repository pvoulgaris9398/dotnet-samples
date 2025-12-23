using BenchmarkDotNet.Attributes;

namespace BenchmarkResearch
{
    public class StringLoaderBenchmark
    {
        [Benchmark]
        public void Sleep() => Thread.Sleep(10);

        [Benchmark(Description = "Testing")]
        public void SleepSomeMore() => Thread.Sleep(10);
    }
}