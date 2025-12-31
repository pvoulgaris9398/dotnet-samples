using System.Globalization;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

/*
Adapted from: https://blog.ndepend.com/improve-c-code-performance-with-spant/
*/
namespace BenchmarkResearch
{
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class StringParsingBenchmark
    {
        // We want to avoid allocating arrays to fill during benchmarks
        // thus NBUint pre-determines their length
        const int NBUint = 4;
        const string CommaSeparatedUInt = "163,496,691,1729";

        readonly uint[] _arrayToFill1 = new uint[NBUint];

        [Benchmark(Baseline = true)]
        public void GetUIntArrayWithSplit() =>
            GetUIntArrayWithStringSplit(CommaSeparatedUInt, _arrayToFill1);

        readonly uint[] _arrayToFill2 = new uint[NBUint];

        [Benchmark]
        public void GetUIntArrayWithSpan() =>
            GetUIntArrayWithSpan(CommaSeparatedUInt, _arrayToFill2);

        readonly uint[] _arrayToFill3 = new uint[NBUint];

        [Benchmark]
        public void GetUIntArrayWithAstuteParsing() =>
            GetUIntArrayWithAstuteParsing(CommaSeparatedUInt, _arrayToFill3);

        static uint[] GetUIntArrayWithStringSplit(string commaSeparatedUInt, uint[] arrayToFill)
        {
            // Split() allocates an array and 4x strings
            string[] arrayOfString = commaSeparatedUInt.Split(',');
            var length = arrayOfString.Length;
            for (int i = 0; i < length; i++)
            {
                arrayToFill[i] = uint.Parse(arrayOfString[i], CultureInfo.InvariantCulture);
            }
            return arrayToFill;
        }

        static void GetUIntArrayWithSpan(string commaSeparatedUInt, uint[] arrayToFill)
        {
            // View the string as a span, so we can slice it in loop
            ReadOnlySpan<char> span = commaSeparatedUInt.AsSpan();
            int nextCommaIndex = 0;
            int insertValAtIndex = 0;
            bool isLastLoop = false;
            while (!isLastLoop)
            {
                int indexStart = nextCommaIndex;
                nextCommaIndex = commaSeparatedUInt.IndexOf(',', indexStart);

                isLastLoop = (nextCommaIndex == -1);
                if (isLastLoop)
                {
                    nextCommaIndex = commaSeparatedUInt.Length; // Parse last uint
                }

                // Get a slice of the string that contains the next uint...
                ReadOnlySpan<char> slice = span.Slice(indexStart, nextCommaIndex - indexStart);
                // ... and parse it
                uint valParsed = uint.Parse(slice, CultureInfo.InvariantCulture);

                // Then insert valParsed in arrayToFill
                arrayToFill[insertValAtIndex] = valParsed;
                insertValAtIndex++;

                // Skip the comma for next iteration
                nextCommaIndex++;
            }
        }

        static void GetUIntArrayWithAstuteParsing(string commaSeparatedUInt, uint[] arrayToFill)
        {
            var length = commaSeparatedUInt.Length;
            int insertValAtIndex = 0;
            int valParsed = 0; // Don't use a uint to avoid casting in astute parsing formula
            for (int i = 0; i < length; i++)
            {
                char @char = commaSeparatedUInt[i];
                if (@char != ',')
                {
                    // Astute Parsing: Modify valParsed from the actual @char
                    valParsed = valParsed * 10 + (@char - '0');
                    continue;
                }
                // A comma is an opportunity to insert valParsed in arrayToFill
                arrayToFill[insertValAtIndex] = (uint)valParsed;
                insertValAtIndex++;
                valParsed = 0;
            }
            // Insert last valParsed
            arrayToFill[insertValAtIndex] = (uint)valParsed;
        }
    }
}
