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

        public static void Test2()
        {
            var data = TestData2.Select(i => int.Parse(i.ToString(), CultureInfo.InvariantCulture));
            var result = data.Generate().ToList();

            foreach (var item in result)
            {
                Write($"{item?.ToString(CultureInfo.InvariantCulture) ?? "."}");
            }
            WriteLine();
        }

        public static void Test1()
        {
            var data = TestData1.Select(i => int.Parse(i.ToString(), CultureInfo.InvariantCulture));
            var result = data.Generate().ToList();

            foreach (var item in result)
            {
                Write($"{item?.ToString(CultureInfo.InvariantCulture) ?? "."}");
            }
            WriteLine();
        }

        public static void Test0()
        {
            var data = TestData1.Select(i => int.Parse(i.ToString(), CultureInfo.InvariantCulture));
            var show = true;
            var index = 0;
            foreach (var item in data)
            {
                foreach (var i in Enumerable.Range(0, item))
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

        private static IEnumerable<int?> Generate(this IEnumerable<int> data)
        {
            var show = true;
            var index = 0;
            foreach (var item in data)
            {
                foreach (var i in Enumerable.Range(0, item))
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

            WriteLine($"Data: {data}");
        }
    }
}