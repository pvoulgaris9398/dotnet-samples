using System.Text.RegularExpressions;

namespace Algorithms
{
    public static class Common
    {
#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.
#pragma warning disable CA1305 // Specify IFormatProvider
        public static List<int> ParseIntsNoSign(this string line) => [.. Regex.Matches(line, @"\d+").Select(static match => int.Parse(match.Value))];
#pragma warning restore CA1305 // Specify IFormatProvider
#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.

        public static (T a, T b) ToPair<T>(this List<T> list)
        {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
            return list switch
            {
                [T a, T b] => (a, b),
                _ => throw new ArgumentException()
            };
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
        }

        public static List<List<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> values) =>
        values.Aggregate(
            new List<List<T>>(),
            (acc, row) =>
            {
                var i = 0;
                foreach (var cell in row)
                {
                    if (acc.Count <= i) acc.Add([]);
                    acc[i++].Add(cell);
                }
                return acc;
            });

        public static IEnumerable<string> ReadLines(this TextReader reader)
        {
            while (reader.ReadLine() is string line)
            {
                yield return line;
            }
        }
    }
}
