namespace DotNetSampleApp
{
    /// <summary>
    /// https://stackoverflow.com/questions/73475521/benchmarkdotnet-inprocessemittoolchain-complete-sample
    /// </summary>
    public class AntiVirusFriendlyConfig : ManualConfig
    {
#pragma warning disable CS0414 // Remove unused private members
        private readonly string _notUsed = "";
#pragma warning restore CS0414 // Remove unused private members
        public AntiVirusFriendlyConfig()
        {
            var _ = AddJob(Job.MediumRun.WithToolchain(InProcessNoEmitToolchain.Instance));
        }
    }
}
