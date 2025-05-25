using System.Globalization;
using System.Text.RegularExpressions;

namespace Advent2024
{
    public static class CommonFuncs
    {
        /// <summary>
        /// TODO: Properly implement
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<(char, char)> ToPairs(this string list)
        {
            if (list == null)
            {
                yield break;
            }
            yield return ('1', '1');
        }

        /// <summary>
        /// TODO: Properly implement
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<(int, int)> ToPairs(this List<int> list)
        {
            if (list == null)
            {
                yield break;
            }
            yield return (1, 1);
        }
        public static char[][] ReadMap(this List<string> lines) =>
            [.. lines.Select(line => line.ToCharArray())];

        public static IEnumerable<string> Antidiagonals(this IEnumerable<string> data, int rows, int cols) =>
            data.Reverse().Diagonals(rows, cols);

        private static string Diagonal(this IEnumerable<string> data, int startRow, int startCol, int cols) =>
#pragma warning disable IDE0305 // Simplify collection initialization
            new(data.Skip(startRow).Take(cols - startCol).Select((row, i) => row[startCol + i]).ToArray());
#pragma warning restore IDE0305 // Simplify collection initialization

        public static IEnumerable<string> Diagonals(this IEnumerable<string> data, int rows, int cols) =>
            Enumerable.Range(0, cols).Select(col => data.Diagonal(0, col, cols))
                .Concat(Enumerable.Range(1, rows - 1).Select(row => data.Diagonal(row, 0, cols)));

        public static IEnumerable<string> Rows(this IEnumerable<string> data) => data;

        public static IEnumerable<string> Columns(this IEnumerable<string> data, int cols) =>
            Enumerable.Range(0, cols).Select(i => new string([.. data.Select(row => row[i])]));


        public static List<int> ParseIntsNoSign(this string line) =>
        [
            .. Regex.Matches(line, @"\d+").Select(static match => int.Parse(match.Value, CultureInfo.InvariantCulture.NumberFormat))
        ];

        public static List<long> ParseLongsNoSign(this string line) =>
            [.. Regex.Matches(line, @"\d+").Select(static match => long.Parse(match.Value, CultureInfo.InvariantCulture.NumberFormat))];


        public static (T a, T b) ToPair<T>(this List<T> list)
        {
            return list switch
            {
                [T a, T b] => (a, b),
                _ => throw new ArgumentException()
            };
        }

        public static List<string> LoadFileData(string path) => [.. File.OpenText(path).ReadLines()];


        public static List<List<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> values) =>
            values.Aggregate(
                new List<List<T>>(),
                (acc, row) =>
                {
                    var i = 0;
                    foreach (var cell in row)
                    {
                        if (acc.Count <= i)
                            acc.Add([]);

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
