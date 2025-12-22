using System.Diagnostics;
using System.Globalization;

namespace Advent2024
{
    internal static class Day09Alternate
    {
#pragma warning disable IDE0051 // Remove unused private members
        private static string TestData1 => "12345";
#pragma warning restore IDE0051 // Remove unused private members
        private static string TestData2 => "2333133121414131402";

        /// <summary>
        /// 0..111....22222
        /// </summary>
        private static string RealData => File.OpenText("..\\..\\..\\..\\..\\day9.txt").ReadToEnd();

        public static void Run(bool testing = false)
        {
            WriteLine(new string('*', 80));
            WriteLine($"{nameof(Day09Alternate)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            Stopwatch stopwatch1 = Stopwatch.StartNew();

            var data = (testing ? TestData2 : RealData).Select(i =>
                long.Parse(i.ToString(), CultureInfo.InvariantCulture)
            );
            var disk = data.Generate().ToArray();
            Compact(disk);

            var checksum = disk.Checksum();

            stopwatch1.Stop();

            WriteLine($"{nameof(checksum)}: {checksum} in {stopwatch1} ms");
        }

        private static long Checksum(this long?[] disk)
        {
            long checksum = 0;
            int counter = 0;
            foreach (var block in disk)
            {
                checksum += counter++ * (block ?? 0);
            }
            return checksum;
        }

        private static void Compact(long?[] disk)
        {
            int nextSpace = 0;
            int nextBlock = disk.Length - 1;

            while (nextSpace < nextBlock)
            {
                if (disk[nextSpace] != null)
                {
                    nextSpace++;
                }
                if (disk[nextBlock] == null)
                {
                    nextBlock--;
                }
                if (disk[nextSpace] == null && disk[nextBlock] != null)
                {
                    Swap(ref disk[nextSpace], ref disk[nextBlock]);
                }
            }
        }

        private static void Swap(ref long? x, ref long? y) => (y, x) = (x, y);

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
    }
}
