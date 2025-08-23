namespace Algorithms
{
    internal static class Functional
    {
        internal static void Test1(string inputToParse)
        {
            var result = inputToParse.ParseInt();

            result.Match(
                value => WriteLine(value),
                () => WriteLine("Unable to parse: {0}!", inputToParse));
        }
    }
}
