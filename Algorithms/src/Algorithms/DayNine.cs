using System.Globalization;

namespace Algorithms
{
    internal static class DayNine
    {
        private static string TestData1 => "12345";
        private static string TestData2 => "2333133121414131402";

        /// <summary>
        /// 0..111....22222
        /// </summary>

        private static string RealData => File.OpenText("..\\..\\..\\..\\..\\day9.txt").ReadToEnd();

        public static void Test4()
        {
            long?[] data = [0, 0, 9, 9, 8, 1, 1, 1, 8, 8, 8, 2, 7, 7, 7, 3, 3, 3, 6, 4, 4, 6, 5, 5, 5, 5, 6, 6];
            var expected = 1928;
            var actual = data.CalculateChecksum();

            WriteLine($"Expected: {expected} = Actual: {actual}");
        }

        public static void Test3()
        {
            long?[] data = [0, 0, 9, 9, 8];
            /*  0 * 0 = 0
             *  1 * 0 = 0
             *  2 * 9 = 18
             *  3 * 9 = 27
             *  4 * 8 = 32
             *  Sum = 77
             */
            var expected = 77;
            var actual = data.CalculateChecksum();

            WriteLine($"Expected: {expected} = Actual: {actual}");

        }

        public static void Test2()
        {
            var data = TestData2.Select(i => long.Parse(i.ToString(), CultureInfo.InvariantCulture));
            var result = data.Generate().ToList();

            foreach (var item in result)
            {
                Write($"{item?.ToString(CultureInfo.InvariantCulture) ?? "."}");
            }
            WriteLine();
        }

        public static void Test1()
        {
            var data = TestData1.Select(i => long.Parse(i.ToString(), CultureInfo.InvariantCulture));
            var result = data.Generate().ToList();

            foreach (var item in result)
            {
                Write($"{item?.ToString(CultureInfo.InvariantCulture) ?? "."}");
            }
            WriteLine();
        }

        public static void Test0()
        {
            var data = TestData1.Select(i => long.Parse(i.ToString(), CultureInfo.InvariantCulture));
            var show = true;
            var index = 0;
            foreach (var item in data)
            {
                foreach (var i in Enumerable.Range(0, (int)item))
                {
                    if (show)
                    {
                        Write($"{index / 2}");
                    }
                    else
                    {
                        Write(".");
                    }

                }
                show = !show;
                index++;
            }
            WriteLine();
        }



        private static IEnumerable<long?> MoveBlocks(this IEnumerable<long?> data)
        {
            var blankCount = data.Count(i => i == null) - 1;
            var copy = data;

            while (blankCount > 0)
            {
                foreach (var element in data)
                {
                    if (blankCount == 0)
                    {
                        yield return null;
                    }
                    else if (element == null)
                    {
                        blankCount--;
                        var temp = copy.Last(i => i != null);
                        yield return temp;
                        copy = copy.Take(copy.Count() - 1);


                    }
                    else
                    {
                        yield return element;
                    }
                }
            }
        }

        private static long CalculateChecksum(this IEnumerable<long?> data) =>
         data.Select((row, index) => index * (row ?? 0)).Sum();

        private static IEnumerable<long?> Generate(this IEnumerable<long> data)
        {
            var show = true;
            var index = 0;
            foreach (var item in data)
            {
                foreach (var i in Enumerable.Range(0, (int)item))
                {
                    yield return show ? Convert.ToChar(index / 2) : null;

                }
                show = !show;
                index++;
            }
        }

        public static void Run(bool testing = false)
        {
            WriteLine(new string('*', 80));
            WriteLine($"{nameof(DayNine)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            var data = testing ? TestData1 : RealData;

            var checksum = data
                            .Select(i => long.Parse(i.ToString(), CultureInfo.InvariantCulture))
                            .Generate()
                            .MoveBlocks()
                            .CalculateChecksum();

            WriteLine($"{nameof(checksum)}: {checksum}");
        }
    }
}