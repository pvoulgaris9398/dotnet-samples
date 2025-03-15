using Investments;
using Securities;
namespace DotNetObservableExample
{
    public static class TestSix
    {
        public static void Execute()
        {
            Say.hello("Marie");

            var input = "ABCDEFG";
            var cusip = input.ToCusip();

            Console.WriteLine(cusip == null ? $"'{input}' is not a valid Cusip" : cusip);

            var security = Factory.CreateValidSecurity("ABC")?.Value;

            Console.WriteLine(security);

        }
    }
}
