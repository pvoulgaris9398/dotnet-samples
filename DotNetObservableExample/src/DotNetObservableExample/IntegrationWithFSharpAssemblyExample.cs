namespace DotNetObservableExample
{
    public static class IntegrationWithFSharpAssemblyExample
    {
        public static string Description() => "Demonstrate Integration With Assembly Written In F#";

        public static void Execute()
        {
            Say.hello("Marie");

            var input = "38259P508";
            var cusip = input.ToCusip();

            Console.WriteLine(cusip == null ? $"'{input}' is not a valid Cusip" : cusip);

            var security = Factory.CreateValidSecurity("ABC")?.Value;

            Console.WriteLine(security);
        }
    }
}
