using BenchmarkDotNet.Attributes;

namespace DotNetSampleApp
{
    [Config(typeof(AntiVirusFriendlyConfig))]
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class BenchmarkPerformance
    {
        [Params(100, 200)]
        private readonly int N = 1;

        private string _countries = "";
        private int _index;
        private int _numberOfCharactersToExtract;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _countries = "India, USA, UK, Australia, Netherlands, Belgium";
#pragma warning disable CA1865 // Use 'string.Method(char)' instead of 'string.Method(string)' for string with single char
            _index = _countries.LastIndexOf(",", StringComparison.Ordinal);
#pragma warning restore CA1865 // Remove unused private members
            _numberOfCharactersToExtract = _countries.Length - _index;
        }

        [Benchmark]
        public void SubString()
        {
            for (var i = 0; i < N; i++)
            {
                _ = _countries.Substring(_index + 1, _numberOfCharactersToExtract - 1);
            }
        }

        [Benchmark(Baseline = true)]
        public void Span()
        {
            for (var i = 0; i < N; i++)
            {
                _ = _countries.AsSpan().Slice(_index + 1, _numberOfCharactersToExtract - 1);
            }
        }
    }
}
