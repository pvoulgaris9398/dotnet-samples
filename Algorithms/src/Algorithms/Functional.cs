namespace Algorithms;

internal static class Functional
{
    internal static void Test1(string inputToParse)
    {
        OptionType<int> result = inputToParse.ParseInt();

        result.Match(
            WriteLine,
            () => WriteLine("Unable to parse: {0}!", inputToParse));
    }
}
