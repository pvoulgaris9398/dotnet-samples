namespace Algorithms
{
    internal static class OptionTypeExtensions
    {
        public static OptionType<int> ParseInt(this string str) =>
            int.TryParse(str, out var result)
            ? OptionType<int>.Some(result)
            : OptionType<int>.None();
    }
}
