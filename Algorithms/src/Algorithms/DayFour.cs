using System.Text.RegularExpressions;

namespace Algorithms
{
    internal static class DayFour
    {
        internal static void RunNew()
        {
            var searchTerm = "XMAS";

            var data = LoadFileData("..\\..\\..\\..\\..\\day4.txt");
            int rows = data.Count;
            int columns = data[0].Length;

            int count = data.GetAllStrings(rows, columns).Sum(s => s.CountWord("XMAS"));
            int xcount = data.GetAllXes(rows, columns).Count(x => Valid.Contains(x));

            Console.WriteLine(new string('*', 80));
            Console.WriteLine(nameof(RunNew));
            Console.WriteLine($"Count of {searchTerm} in the Dataset is {count}");
            Console.WriteLine($"Count Valid X's is {xcount}");

        }

        /* I thought about munging the data into one _long_ string and doing the search that way
         * (for about two seconds)
         * Quickly realized I would lose the context and miss-out on horizontals and diagonals
         * So that won't work.
         * This is my original work...
         */
        internal static void RunOriginal()
        {
            var searchTerm = "XMAS";

            var count =
                //GetDataForTesting()
                LoadFileData("..\\..\\..\\..\\..\\day4.txt")
                .AsArray()
                .Transform()
                .CountInstancesOf(searchTerm);

            Console.WriteLine(new string('*', 80));
            Console.WriteLine(nameof(RunOriginal));
            Console.WriteLine($"Count of {searchTerm} in the Dataset is {count}");

        }
        private static readonly string[] Valid = ["MASMAS", "MASSAM", "SAMMAS", "SAMSAM"];

        private static IEnumerable<string> GetAllXes(this List<string> matrix, int rows, int cols) =>
            GetXCenters(rows, cols).Select(center => matrix.GetX(center.row, center.col));

        private static IEnumerable<(int row, int col)> GetXCenters(int rows, int cols) =>
            Enumerable.Range(1, rows - 2).SelectMany(row => Enumerable.Range(1, cols - 2).Select(col => (row, col)));

        private static string GetX(this List<string> matrix, int row, int col) => new string([
        matrix[row - 1][col - 1], matrix[row][col], matrix[row + 1][col + 1],
        matrix[row - 1][col + 1], matrix[row][col], matrix[row + 1][col - 1]]);
        private static int CountWord(this string s, string word) =>
        Regex.Matches(s, Regex.Escape(word)).Count;

        private static IEnumerable<string> GetAllStrings(this IEnumerable<string> data, int rows, int cols) =>
        data.Rows().Concat(data.Columns(cols)).Concat(data.Diagonals(rows, cols)).Concat(data.Antidiagonals(rows, cols)).TwoWay();
        private static IEnumerable<string> TwoWay(this IEnumerable<string> strings) =>
#pragma warning disable IDE0305 // Simplify collection initialization
       strings.SelectMany(s => new[] { s, new string(s.Reverse().ToArray()) });
#pragma warning restore IDE0305 // Simplify collection initialization
        private static IEnumerable<string> Rows(this IEnumerable<string> data) => data;

        private static IEnumerable<string> Columns(this IEnumerable<string> data, int cols) =>
#pragma warning disable IDE0305 // Simplify collection initialization
        Enumerable.Range(0, cols).Select(i => new string(data.Select(row => row[i]).ToArray()));
#pragma warning restore IDE0305 // Simplify collection initialization

        private static IEnumerable<string> Diagonals(this IEnumerable<string> data, int rows, int cols) =>
        Enumerable.Range(0, cols).Select(col => data.Diagonal(0, col, cols))
            .Concat(Enumerable.Range(1, rows - 1).Select(row => data.Diagonal(row, 0, cols)));

        private static IEnumerable<string> Antidiagonals(this IEnumerable<string> data, int rows, int cols) =>
            data.Reverse().Diagonals(rows, cols);

        private static string Diagonal(this IEnumerable<string> data, int startRow, int startCol, int cols) =>
#pragma warning disable IDE0305 // Simplify collection initialization
            new(data.Skip(startRow).Take(cols - startCol).Select((row, i) => row[startCol + i]).ToArray());
#pragma warning restore IDE0305 // Simplify collection initialization

        internal static List<string> Transform(this List<List<char>> data)
        {

            List<string> result = [];

            //Console.WriteLine(new string('*', 80));
            //Console.WriteLine("Horizontals");

            var horizontals = data.Select((row, rowIndex) =>
            {
#pragma warning disable IDE0305 // Simplify collection initialization
                var result = new string(row.ToArray());
#pragma warning restore IDE0305 // Simplify collection initialization
                //Console.WriteLine(result);

                return result;

            }).ToList();

            // https://stackoverflow.com/questions/42172408/get-all-diagonals-in-a-2-dimensional-data-using-linq
            //Console.WriteLine(new string('*', 80));
            //Console.WriteLine("Left Diagonals");
            var leftDiagonals = data.SelectMany((row, rowIdx) =>
                row.Select((x, colIdx) => new { Key = rowIdx - colIdx, Value = x }))
                    .GroupBy(x => x.Key)
                    .OrderBy(x => x.Key)
                    .Select(values =>
                    {
#pragma warning disable IDE0305 // Simplify collection initialization
                        var result = new string(values.Select(i => i.Value).ToArray());
#pragma warning restore IDE0305 // Simplify collection initialization
                        //Console.WriteLine(result);
                        return result;
                    })
                    .ToList();

            // https://stackoverflow.com/questions/42172408/get-all-diagonals-in-a-2-dimensional-data-using-linq
            //Console.WriteLine(new string('*', 80));
            //Console.WriteLine("Right Diagonals");
            var rightDiagonalsOriginal = data
                .SelectMany((row, rowIdx) =>
                row.Select((x, colIdx) => new { Key = colIdx - rowIdx, Value = x }))
                    .GroupBy(x => x.Key)
                    //.OrderBy(x => x.Key)
                    .Select(values =>
                    {
#pragma warning disable IDE0305 // Simplify collection initialization
                        var result = new string(values.Select(i => i.Value).ToArray());
#pragma warning restore IDE0305 // Simplify collection initialization
                        //Console.WriteLine(result);
                        return result;
                    })
                    .ToList();

            //Console.WriteLine(new string('*', 80));
            //Console.WriteLine("Verticals");
            var verticals = data.SelectMany((row, rowIdx) =>
                row.Select((x, colIdx) => new { Key = colIdx, Value = x }))
                    .GroupBy(x => x.Key)
                    .OrderBy(x => x.Key)
                    .Select(values =>
                    {
#pragma warning disable IDE0305 // Simplify collection initialization
                        var result = new string(values.Select(i => i.Value).ToArray());
#pragma warning restore IDE0305 // Simplify collection initialization
                        //Console.WriteLine(result);
                        return result;
                    })
                    .ToList();

            /*  This way seems to be the key. My original way above (rightDiagonalsOriginal)
             *  was coming-up with 32 less record(s) overall than the correct way
             *  I think my understanding of what I was doing was a little flawed.
             *  This way works fine though.
             */
            data.Reverse(); //<<<===KEY
            var rightDiagonalsNew = data.SelectMany((row, rowIdx) =>
               row.Select((x, colIdx) => new { Key = rowIdx - colIdx, Value = x }))
                   .GroupBy(x => x.Key)
                   .OrderBy(x => x.Key)
                   .Select(values =>
                   {
#pragma warning disable IDE0305 // Simplify collection initialization
                       var result = new string(values.Select(i => i.Value).ToArray());
#pragma warning restore IDE0305 // Simplify collection initialization
                       //Console.WriteLine(result);
                       return result;
                   })
                   .ToList();

            result.AddRange(horizontals);
            result.AddRange(verticals);
            result.AddRange(leftDiagonals);
            result.AddRange(rightDiagonalsNew);

            return result;
        }

        internal static int CountInstancesOf(this List<string> list, string searchWord)
        {
            /* search
             * length = x
             * m (10) row(s)
             * n (10) columns()
             * n * n diagonals() - those less than x long
             * 
             */

            // Search backwards and forward
            var forward = searchWord;
#pragma warning disable IDE0305 // Simplify collection initialization
            var backwards = new string(searchWord.Reverse().ToArray());
#pragma warning restore IDE0305 // Simplify collection initialization

            var forwardCount = list
                .Sum(line => Regex.Matches(line, Regex.Escape(forward)).Count);

            var backwardsCount = list
                .Sum(line => Regex.Matches(line, Regex.Escape(backwards)).Count);

            return forwardCount + backwardsCount;

        }

        internal static List<List<char>> AsArray(this List<string> lines)
        {
            var data = new List<List<char>>();

            foreach (var line in lines)
            {
                var array = new List<char>(line.ToCharArray());
                data.Add(array);
            }

            return data;

        }

        internal static char[,] ToArray_OLD()
        {
            var lines = GetDataForTesting();
            int m = lines[0].Length;
            int n = lines.Count;
            char[,] result = new char[m, n];

            for (int i = 0; i < m; i++)
            {
                var lineArray = lines[i].ToArray();
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = lineArray[j];
                }
            }

            return result;
        }

        internal static List<string> LoadFileData(string path) => [.. File.OpenText(path).ReadLines()];

        internal static List<string> GetDataForTesting()
        {
            return [
                 "MMMSXXMASM"
                ,"MSAMXMSMSA"
                ,"AMXSXMAAMM"
                ,"MSAMASMSMX"
                ,"XMASAMXAMM"
                ,"XXAMMXXAMA"
                ,"SMSMSASXSS"
                ,"SAXAMASAAA"
                ,"MAMMMXMMMM"
                ,"MXMXAXMASX"
                    ];
        }
    }
}
