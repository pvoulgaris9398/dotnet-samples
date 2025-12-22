#!/usr/bin/env -S dotnet run

Functions.Test(
    "0e-14",
    (input, out result) => decimal.TryParse(input, out result),
    Console.WriteLine,
    Console.WriteLine
);

Functions.Test(
    "0e-14",
    (input, out result) =>
        decimal.TryParse(
            input,
            System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture,
            out result
        ),
    Console.WriteLine,
    Console.WriteLine
);

Functions.Test(
    "1.2345E-5",
    (input, out result) => decimal.TryParse(input, out result),
    Console.WriteLine,
    Console.WriteLine
);

Functions.Test(
    "1.2345E-5",
    (input, out result) =>
        decimal.TryParse(
            input,
            System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture,
            out result
        ),
    Console.WriteLine,
    Console.WriteLine
);

delegate bool DecimalParseStrategy(string input, out decimal result);

public static class Functions
{
    internal static void Test(
        string input,
        DecimalParseStrategy parser,
        Action<string> success,
        Action<string> failure
    )
    {
        if (parser(input, out var result))
        {
            success($"{input} parses fine to: {result}");
        }
        else
        {
            failure($"Nope {input} - yo no comprende!");
        }
    }
}
