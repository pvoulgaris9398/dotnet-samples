#!/usr/bin/env -S dotnet run

/*
Playing around with C# switch expressions
and new C# 14/.NET 10 extension member syntax
*/

using System.Runtime.InteropServices;

Console.WriteLine($"Running on: {RuntimeInformation.FrameworkDescription}");

Console.WriteLine(new Tester2((0, 777)).Format());
Console.WriteLine(new Tester2((-444, 0)).Format());
Console.WriteLine(new Tester2((667, 888)).Format());
Console.WriteLine(new Tester2((1, 2)).Format());

Console.WriteLine(new Tester1(666).Format());
Console.WriteLine(new Tester1(42).Format());
Console.WriteLine(new Tester1(11).Format());

internal static class Tester1Extensions
{
    internal static string Format(this Tester1 t) =>
        t.Value switch
        {
            42 => "What's the question?",
            11 => "Hello, eleven!",
            _ => "Goodbye!",
        };
}

internal sealed record Tester1(int Value);

internal static class Tester2Extensions
{
    // C# 14 Feature. Note no Access Modifier can be specified here...
    extension(Tester2 t)
    {
        // But an Access Modifier CAN be specified here:
        internal string Format() =>
            t.Value switch
            {
                (0, int y) => "'x' is zero (0)!",
                (int x, 0) => "'y' is zero (0)!",
                (int x, int y) when x >= 666 && y > 0 => $"Hello there: 'x' = {x}, 'y' = {y}",
                (int x, int y) when x < 666 || y < 0 => $"What's up?: 'x' = {x}, 'y' = {y}",
                _ => "Goodbye!",
            };
    }
}

internal sealed record Tester2((int, int) Value);
