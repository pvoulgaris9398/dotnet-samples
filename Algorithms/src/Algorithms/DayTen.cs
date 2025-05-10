namespace Algorithms
{
    internal static class DayTen
    {
        private static string TestData1 => "12345";
        private static string TestData2 => "2333133121414131402";

        /// <summary>
        /// 0..111....22222
        /// </summary>
        /// 

        internal static List<string> TestDataSet1()
        {
            return [
 "...0..."
,"...1..."
,"...2..."
,"6543456"
,"7.....7"
,"8.....8"
,"9.....9"
                ];

        }

        internal static List<string> TestDataSet2()
        {
            return [
 "89010123"
,"78121874"
,"87430965"
,"96549874"
,"45678903"
,"32019012"
,"01329801"
,"10456732"
                ];

        }

        internal static List<string> TestDataSet3()
        {
            return [
 "..90..9"
,"...1.98"
,"...2..7"
,"6543456"
,"765.987"
,"876...."
,"987...."
                ];

        }

        internal static List<string> TestDataSet4()
        {
            return [
"012345"
,"123456"
,"234567"
,"345678"
,"4.6789"
,"56789."
                ];

        }

        public static void Test0()
        {
            var map = LoadTestData(TestDataSet2());

            var positions = map.ToPositions().ToList();

            var routes = positions.Routes().ToList();
            var total = routes.Count;

            WriteLine($"{nameof(total)}: {total}");
        }


        private static List<List<long>> LoadFileData(string path) => [.. File.OpenText(path).ReadLines().Select(row => row.Select(c => long.TryParse(char.ToString(c), out long result) ? result : -1).ToList())];

        private static List<List<long>> LoadTestData(List<string> data) => [.. data.Select(row => row.Select(c => long.TryParse(char.ToString(c), out long result) ? result : -1).ToList())];

        public static void Run(bool testing = false)
        {
            WriteLine(new string('*', 80));
            WriteLine($"{nameof(DayTen)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            var map = testing ? LoadTestData(TestDataSet2()) : LoadFileData("..\\..\\..\\..\\..\\day10.txt");

            var positions = map.ToPositions().ToList();

            var sixes = positions.Where(p => p.Height == 6).ToList();

            var routes = positions.Routes().ToList();
            /* 
             * 5/3/25:
             * 1518 is too high
             * 
             * 1115 is too high also
             * 
             * 84 is too low
             */
            var total = routes.Count;

            var first = routes.Where(p => p.Contains(new Position(0, 2, 0))).ToList();
            var second = routes.Where(p => p.Contains(new Position(0, 4, 0))).ToList();
            var third = routes.Where(p => p.Contains(new Position(2, 4, 0))).ToList();
            var fourth = routes.Where(p => p.Contains(new Position(4, 6, 0))).ToList();
            var fifth = routes.Where(p => p.Contains(new Position(5, 2, 0))).ToList();
            var sixth = routes.Where(p => p.Contains(new Position(5, 5, 0))).ToList(); //xxx
            var seventh = routes.Where(p => p.Contains(new Position(6, 0, 0))).ToList();
            var eighth = routes.Where(p => p.Contains(new Position(6, 6, 0))).ToList();
            var ninth = routes.Where(p => p.Contains(new Position(7, 1, 0))).ToList();

            var last = routes.GroupBy(r => r.Select(p => p)).Distinct().ToList(); //exact

            WriteLine($"{nameof(total)}: {total}");

        }

        private static List<Stack<Position>> Routes(this IEnumerable<Position> positions)
        {
            List<Stack<Position>> routes = [];

            var heights = positions
                .Where(p => p.Height >= 0)
                .Select(p => p.Height).Distinct().OrderBy(p => p).ToList();

            var enumerator = heights.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return routes;
            }

            var startHeight = enumerator.Current;
            foreach (var height in positions.Where(p => p.Height == startHeight).ToList())
            {
                if (height == null)
                {
                    continue;
                }
                var stack = new Stack<Position>();
                stack.Push(height ?? new Position(-1, -1, -1));

                routes.Add(stack);
            }
            while (enumerator.MoveNext())
            {

                var next = positions.Where(p => p.Height == enumerator.Current).ToList();

                routes = routes.AddNext(next).ToList();

                var tester = routes.ToList();

            }

            return routes;
        }

        private static IEnumerable<Stack<Position>> AddNext(
            this List<Stack<Position>> accumulator,
            IEnumerable<Position> next)
        {

            foreach (var stack in accumulator)
            {
                var position = stack.Peek();

                var near = next.Where(p => p.NextTo(position)).ToList();

                if (near.Count == 0)
                {
                    continue;
                }
                else if (near.Count == 1)
                {
                    stack.Push(near.Skip(0).First());
                    yield return stack;
                }
                else if (near.Count == 2)
                {
                    var newStack1 = stack.Clone();

                    stack.Push(near.Skip(0).First());
                    yield return stack;

                    newStack1.Push(near.Skip(1).First());
                    yield return newStack1;

                }
                else if (near.Count == 3)
                {
                    var newStack1 = stack.Clone();
                    var newStack2 = stack.Clone();

                    stack.Push(near.Skip(0).First());
                    yield return stack;

                    newStack1.Push(near.Skip(1).First());
                    yield return newStack1;

                    newStack2.Push(near.Skip(2).First());
                    yield return newStack2;

                }

            }

            yield break;
        }

        private static Stack<Position> Clone(this Stack<Position> stack)
        {
            var newStack = new Stack<Position>();
            foreach (var position in stack)
            {
                newStack.Push(position);
            }
            return newStack;
        }

        private static IEnumerable<Position> ToPositions(this List<List<long>> data) =>
            data.SelectMany((row, rowIndex) => row.Select((column, columnIndex) => new Position(rowIndex, columnIndex, column)));

        private sealed record Position(int RowIndex, int ColumnIndex, long Height)
        {
            public bool StartingPosition => Height == 0;
            public bool NextTo(Position other) =>
                (other.RowIndex == RowIndex
                && Math.Abs(other.ColumnIndex - ColumnIndex) == 1)
                ||
                (other.ColumnIndex == ColumnIndex
                 && Math.Abs(other.RowIndex - RowIndex) == 1);
        }

    }
}