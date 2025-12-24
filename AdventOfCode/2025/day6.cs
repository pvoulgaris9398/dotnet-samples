#!/usr/bin/env -S dotnet run
#:project ./AdventOfCode/AdventOfCode.csproj

using System.Globalization;

if (args.Length > 0)
{
    Action actionToExecute = args[0] switch
    {
        "test0" => Tests.Test0,
        "test1" => Tests.Test1,
        "test2" => Tests.Test2,
        "test3" => Tests.Test3,
        _ => () => Console.WriteLine("Unrecognized command-line argument"),
    };
    actionToExecute();
    return;
}

Console.WriteLine($"{Solution.Source}: {Solution.Name}");

Console.WriteLine(new string('*', 80));

Console.WriteLine($"Part 1 (Expecting: 6,343,365,546,996): {Solution.SolvePart1():#,##0}");

Console.WriteLine($"Part 2 (Expecting: ???): {Solution.SolvePart2():#,##0}");

internal sealed class Solution
{
    internal static long SolvePart1(bool debug = false)
    {
        var data = debug ? Test : Lines;

        return data.ToMatrix().Evaluate().Sum();
    }

    /*

    From the data shown below, I was struggling to see how the last
    column of values became:
    
    4 + 431 + 623 = 1058

    While the first column was:

    356 * 24 * 1 = 8544

    And then realized that the additions are left-aligned and
    the multiplications are right-aligned

    ...coming back to this, I realized this is NOT true:
    The spaces are what is formatting within the sections

    I think I just need to replace any blank spaces vertically
    with zero's for the additions and one's for the multiplications

    ...after a few moments thought...Nope, that won't work

    123 328  51 64
     45 64  387 23
      6 98  215 314
    *   +   *   +

    11159870183914 => too high

    */
    internal static long SolvePart2(bool debug = false)
    {
        var data = debug ? Test : Lines;

        return data.ToMatrix().Evaluate(true).Sum();
    }

    private const int Year = 2025;
    private const int Day = 6;
    internal static string Source => $"Advent of Code - {Year}";
    internal static string Name => $"Day {Day}";
    internal static string FileName => $"./AdventOfCode/2025/data/day{Day}.txt";
    internal static string TestFileName => $"./data/day{Day}-test.txt";
    internal static List<string> Test => [.. File.OpenText(TestFileName).ReadLines()];
    internal static List<string> Lines => [.. File.OpenText(FileName).ReadLines()];
}

internal static class Extensions
{
    extension(TextReader reader)
    {
        internal IEnumerable<string> ReadLines()
        {
            ArgumentNullException.ThrowIfNull(reader);
            while (reader.ReadLine() is string line)
            {
                yield return line;
            }
        }
    }

    internal static IEnumerable<long> ToLongs(this string line) =>
        line.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries)
            .Where(line => long.TryParse(line, out long result))
            .Select(long.Parse);

    internal static IEnumerable<MathOperation> ToOperations(this string line) =>
        line.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries)
            .Where(line => !long.TryParse(line, out long result))
            .Select(item =>
                (MathOperation)(
                    item.Equals("+", StringComparison.Ordinal)
                        ? new Addition(0, 0)
                        : new Multiplication(0, 0)
                )
            );

    internal static IEnumerable<IEnumerable<long>> Numbers(this IEnumerable<string> lines) =>
        lines.Select(line => line.ToLongs()).Where(num => num.Any());

    internal static IEnumerable<IEnumerable<MathOperation>> Operations(
        this IEnumerable<string> lines
    ) => lines.Select(line => line.ToOperations()).Where(op => op.Any());

    internal static string[][] ToMatrix(this IEnumerable<string> lines) =>
        [.. lines.Select(line => line.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries))];

    internal static IEnumerable<long> Evaluate(this string[][] matrix, bool rightToLeft = false)
    {
        foreach (int index in Enumerable.Range(0, matrix[0].Length))
        {
            var values = matrix.GetValuesAt(index).ToArray();

            Func<long, string, long> op = values[^1] switch
            {
                "+" => (arg1, arg2) =>
                    arg1 + long.Parse(arg2, NumberStyles.Number, CultureInfo.InvariantCulture),
                "*" => (arg1, arg2) =>
                    arg1 * long.Parse(arg2, NumberStyles.Number, CultureInfo.InvariantCulture),
                var invalid => throw new InvalidDataException($"Invalid: '{invalid}'"),
            };

            long identity = values[^1] switch
            {
                "+" => 0L, // Additive Identity is zero (0)
                "*" => 1L, // Multiplicative Identity is one (1)
                var invalid => throw new InvalidDataException($"Invalid: '{invalid}'"),
            };

            values = values[0..^1];

            if (rightToLeft)
            {
                values =
                [
                    .. values.Transpose(
                        identity == 0 ? new Addition(0, 0) : new Multiplication(0, 0)
                    ),
                ];
            }

            long result = values[..].Aggregate(identity, (total, current) => op(total, current));

            yield return result;
        }
    }

    internal static IEnumerable<string> GetValuesAt(this string[][] matrix, int index)
    {
        foreach (var row in matrix)
        {
            yield return row[index];
        }
    }

    internal static IEnumerable<string> AddPadding(this string[] values, MathOperation op)
    {
        var maxLength = values.Max(v => v.Length);
        var result = values.Select(value =>
            op is Multiplication ? value.PadLeft(maxLength, ' ') : value.PadRight(maxLength, ' ')
        );
        return result;
    }

    internal static IEnumerable<string> Transpose(this string[] values, MathOperation op)
    {
        var padded = values.AddPadding(op);
        var maxLength = values.Max(v => v.Trim().Length);

        foreach (int index in Enumerable.Range(0, maxLength))
        {
            var chars = padded
                ?.Where(value => char.IsDigit(value[index]))
                .Select(c => c[index])
                .ToArray();
            if (chars is not null)
            {
                yield return new string(chars);
            }
        }
    }

    extension(Solution solution)
    {
        /*
        Not visible to the Solution class, I need to understand why
        */
        internal static List<string> Lines => [.. File.OpenText(Solution.FileName).ReadLines()];

        // internal static List<long> ToIntegers(string line)
        // => string.Split(null, StringSplitOptions.RemoveEmptyEntries)
        // .Where(token => long.TryPr)
    }
}

internal abstract record MathOperation(int Begin, int End)
{
    internal static MathOperation Create(char op, int begin, int end) =>
        op switch
        {
            '+' => new Addition(begin, end),
            '*' => new Multiplication(begin, end),
            var invalid => throw new InvalidDataException($"Invalid: '{invalid}'"),
        };
}

internal sealed record Addition(int Begin, int End) : MathOperation(Begin, End);

internal sealed record Multiplication(int Begin, int End) : MathOperation(Begin, End);

internal static class Tests
{
    internal static void Test0()
    {
        var rows = Solution.Lines.Numbers();
        var operations = Solution.Lines.Operations();

        foreach (var row in rows)
        {
            Console.WriteLine($"{row.Count()}");
        }

        foreach (var operation in operations)
        {
            Console.WriteLine($"{operation.Count()}");
        }
    }

    internal static void Test1()
    {
        var matrix = Solution.Test.ToMatrix();

        var results = matrix.Evaluate();

        // foreach (var row in matrix)
        // {
        //     foreach (var column in row)
        //     {
        //         Console.WriteLine(column);
        //     }
        // }

        foreach (var result in results)
        {
            Console.WriteLine(result);
        }
    }

    internal static void Test2()
    {
        string[] data = ["64", "23", "314"];
        var transposed = data.Transpose(new Addition(0, 0));

        foreach (var entry in transposed)
        {
            Console.WriteLine(entry);
        }
    }

    internal static IEnumerable<(char, int)> GetOperators2(this char[] line) =>
        line.Select((index, item) => (index, item));

    internal static IEnumerable<MathOperation> GetOperators(this char[] line)
    {
        IEnumerator<char> enumerator = ((IEnumerable<char>)line).GetEnumerator();

        char op = '\0';
        char prev = '\0';
        int start = 0;
        int end = 0;

        if (enumerator.MoveNext())
        {
            prev = enumerator.Current;
            op = prev;
            start = 0;
            end = 0;
        }

        while (enumerator.MoveNext())
        {
            char current = enumerator.Current;

            if (current is '*' or '+')
            {
                yield return MathOperation.Create(op, start, end);
                op = current;
                start = end + 1;
                end = start;
                continue;
            }
            end += 1;
        }
        yield return MathOperation.Create(op, start, end);
    }

    internal static void Test3()
    {
        var matrix = Solution.Test;
        var operators = Solution.Test[^1].ToCharArray();

        char[] input = [.. "*   +   *   +  "];

        var results = input.GetOperators();

        foreach (var result in results)
        {
            Console.WriteLine(result);
        }
    }
}
