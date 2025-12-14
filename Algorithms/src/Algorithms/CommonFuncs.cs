using System.Text.RegularExpressions;

namespace Algorithms
{
    internal static class CommonFuncs
    {
        public static char[][] ReadMap(this List<string> lines) =>
            [.. lines.Select(line => line.ToCharArray())];

        public static IEnumerable<string> Antidiagonals(this IEnumerable<string> data, int rows, int cols) =>
            data.Reverse().Diagonals(rows, cols);

        private static string Diagonal(this IEnumerable<string> data, int startRow, int startCol, int cols) =>
            new([.. data.Skip(startRow).Take(cols - startCol).Select((row, i) => row[startCol + i])]);

        public static IEnumerable<string> Diagonals(this IEnumerable<string> data, int rows, int cols) =>
            Enumerable.Range(0, cols).Select(col => data.Diagonal(0, col, cols))
                .Concat(Enumerable.Range(1, rows - 1).Select(row => data.Diagonal(row, 0, cols)));

        public static IEnumerable<string> Rows(this IEnumerable<string> data) => data;

        public static IEnumerable<string> Columns(this IEnumerable<string> data, int cols) =>
            Enumerable.Range(0, cols).Select(i => new string([.. data.Select(row => row[i])]));


#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.
#pragma warning disable CA1305 // Specify IFormatProvider
        public static List<int> ParseIntsNoSign(this string line) =>
        [
            .. Regex.Matches(line, @"\d+").Select(static match => int.Parse(match.Value))
        ];
#pragma warning restore CA1305 // Specify IFormatProvider
#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.

#pragma warning disable CA1305 // Specify IFormatProvider
        public static List<long> ParseLongsNoSign(this string line) =>
            [.. Regex.Matches(line, @"\d+").Select(static match => long.Parse(match.Value))];
#pragma warning restore CA1305 // Specify IFormatProvider

        public static (T a, T b) ToPair<T>(this List<T> list) =>
            list switch
            {
                [T a, T b] => (a, b),
                _ => throw new ArgumentException("Unable to parse!")
            };

#pragma warning disable CA2000
        public static List<string> LoadFileData(string path) => [.. File.OpenText(path).ReadLines()];
#pragma warning restore CA2000

        public static List<List<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> values) =>
            values.Aggregate(
                new List<List<T>>(),
                (acc, row) =>
                {
                    int i = 0;
                    foreach (T? cell in row)
                    {
                        if (acc.Count <= i)
                        {
                            acc.Add([]);
                        }

                        acc[i++].Add(cell);
                    }

                    return acc;
                });

        public static IEnumerable<string> ReadLines(this TextReader reader)
        {
            ArgumentNullException.ThrowIfNull(reader);
            while (reader.ReadLine() is string line)
            {
                yield return line;
            }
        }

        public static TextReader ReadFromFile(string path) => File.OpenText(path);
    }
}
