using System.Diagnostics;
using System.Globalization;

namespace Advent2024
{
    internal static class Day09
    {
        private static string TestData1 => "12345";
        private static string TestData2 => "2333133121414131402";

        /// <summary>
        /// 0..111....22222
        /// </summary>
        private static string RealData => File.OpenText("..\\..\\..\\..\\..\\day9.txt").ReadToEnd();

        public static void Test4()
        {
            long?[] data =
            [
                0,
                0,
                9,
                9,
                8,
                1,
                1,
                1,
                8,
                8,
                8,
                2,
                7,
                7,
                7,
                3,
                3,
                3,
                6,
                4,
                4,
                6,
                5,
                5,
                5,
                5,
                6,
                6,
            ];
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
            var data = TestData2.Select(i =>
                long.Parse(i.ToString(), CultureInfo.InvariantCulture)
            );
            var result = data.Generate().ToList();

            foreach (var item in result)
            {
                Write($"{item?.ToString(CultureInfo.InvariantCulture) ?? "."}");
            }
            WriteLine();
        }

        public static void Test1()
        {
            var data = TestData1.Select(i =>
                long.Parse(i.ToString(), CultureInfo.InvariantCulture)
            );
            var result = data.Generate().ToList();

            foreach (var item in result)
            {
                Write($"{item?.ToString(CultureInfo.InvariantCulture) ?? "."}");
            }
            WriteLine();
        }

        public static void Test0()
        {
            var data = TestData1.Select(i =>
                long.Parse(i.ToString(), CultureInfo.InvariantCulture)
            );
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

#pragma warning disable IDE0051 // Remove unused private members
        private static IEnumerable<long?> MoveBlocks(this IEnumerable<long?> data)
#pragma warning restore IDE0051 // Remove unused private members
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
            WriteLine($"{nameof(Day09)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            Stopwatch stopwatch1 = Stopwatch.StartNew();

            string data = testing ? TestData2 : RealData;

            var disk = data.ReadDisk().ToArray();

            long checksum = disk.Compact(MoveBlocks).Sum(file => file.Checksum);

            stopwatch1.Stop();

            long filesMoveChecksum = disk.Compact(MoveFiles).Sum(file => file.Checksum);

            WriteLine($"{nameof(checksum)}: {checksum} in {stopwatch1} ms");
            WriteLine($"{nameof(filesMoveChecksum)}: {filesMoveChecksum}");
        }

        private static int MoveBlocks(FileSection file) => 0;

        private static int MoveFiles(FileSection file) => file.Length;

        private static IEnumerable<Fragment> ReadDisk(this string text)
        {
            int position = 0;
            foreach ((int? fileId, int blocks) in text.ReadSpec())
            {
                // Each type of item here keeps track of it's position
                yield return fileId.HasValue
                    ? new FileSection(fileId.Value, position, blocks)
                    : new Gap(position, blocks);
                position += blocks;
            }
        }

        private static IEnumerable<FileSection> Compact(
            this IEnumerable<Fragment> fragments,
            BlocksConstraint blocksConstraint
        )
        {
            var files = fragments
                .OfType<FileSection>()
                .OrderByDescending(file => file.Position)
                .ToList();
            var gaps = fragments.OfType<Gap>().OrderBy(gap => gap.Position).ToList();

            foreach (FileSection file in files)
            {
                var remainingGaps = new List<Gap>();
                int pendingBlocks = file.Length;

                foreach (var gap in gaps.Where(gap => gap.Position < file.Position))
                {
                    int countOfBlocksToBeMoved = Math.Min(pendingBlocks, gap.Length);

                    // Handles case where we want to move entire files rather than block-by-block
                    if (countOfBlocksToBeMoved < blocksConstraint(file))
                    {
                        remainingGaps.Add(gap);
                        continue;
                    }

                    // Move whatever we can move, update the existing fileId with new position and length
                    if (countOfBlocksToBeMoved > 0)
                    {
                        yield return file with
                        {
                            Position = gap.Position,
                            Length = countOfBlocksToBeMoved,
                        };
                    }

                    // Any remaining blocks?
                    pendingBlocks -= countOfBlocksToBeMoved;

                    // If we did NOT use-up the entire Gap, cache it for next iteration
                    if (gap.Remove(countOfBlocksToBeMoved) is Gap remainder)
                    {
                        remainingGaps.Add(remainder);
                    }
                }

                // If we STILL have any pending blocks that can't be moved, return the new length
                // but don't change it's _Position_
                if (pendingBlocks > 0)
                {
                    yield return file with
                    {
                        Length = pendingBlocks,
                    };
                }

                // Reset gaps to what remains
                gaps = remainingGaps;
            }
        }

        private delegate int BlocksConstraint(FileSection file);

        private abstract record Fragment(int Position, int Length);

        private sealed record FileSection(int FileId, int Position, int Length)
            : Fragment(Position, Length)
        {
            public long Checksum => (long)FileId * Length * ((2 * Position) + Length - 1) / 2;
        }

        private sealed record Gap(int Position, int Length) : Fragment(Position, Length)
        {
            public Gap? Remove(int blocks) =>
                blocks >= Length ? null : new Gap(Position + blocks, Length - blocks);
        }

        private static IEnumerable<(int? fileId, int blocks)> ReadSpec(this string text) =>
            //text.Select(c => (int)(c - '0'))
            text.Select(c => c - '0')
                .Select((int value, int i) => (i % 2 == 0 ? (int?)(i / 2) : null, value));
    }
}
