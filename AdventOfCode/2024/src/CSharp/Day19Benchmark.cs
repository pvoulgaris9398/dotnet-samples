using BenchmarkDotNet.Attributes;

namespace Advent2024
{
#pragma warning disable CA1515 // Consider making public types internal
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class Day19Benchmark
#pragma warning restore CA1515 // Consider making public types internal
    {

        private IEnumerable<string> _towels;

        private IEnumerable<string> _patterns;

        [GlobalSetup]
        public void GlobalSetup()
        {
            (_towels, _patterns) = Day19.Data();
        }

        [Benchmark]
        public void Run()
        {
            Day19.Run((_towels, _patterns));
        }
    }
}
