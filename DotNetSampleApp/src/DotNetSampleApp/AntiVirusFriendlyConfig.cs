using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;

namespace DotNetSampleApp
{
    /// <summary>
    /// https://stackoverflow.com/questions/73475521/benchmarkdotnet-inprocessemittoolchain-complete-sample
    /// </summary>
    public class AntiVirusFriendlyConfig : ManualConfig
    {
#pragma warning disable IDE0051
#pragma warning disable CS0414 // Remove unused private members
        private readonly string _notUsed = "";
#pragma warning restore CS0414 // Remove unused private members
#pragma warning restore IDE0051
        public AntiVirusFriendlyConfig()
        {
            var _ = AddJob(Job.MediumRun
                .WithToolchain(InProcessNoEmitToolchain.Instance));
        }
    }
}
