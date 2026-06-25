#!/usr/bin/env -S dotnet

#:package Humanizer.Core@2.14.1

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Humanizer;

Console.WriteLine(new string('*', 80));

Console.WriteLine($"[Script Output] Starting script execution...");

Console.WriteLine(new string('*', 80));
Console.WriteLine();

// Check if any arguments were passed
if (args.Length == 0)
{
    Console.WriteLine(" -> No arguments were passed to the script.");
}
else
{
    Console.WriteLine($" -> Received {args.Length} argument(s):");
    for (int i = 0; i < args.Length; i++)
    {
        Console.WriteLine($"    [{i}]: {args[i]}");
    }
}

// 1. Fetch the exact source file string via the compiler trace attribute
string scriptPath = GetScriptPath();
string scriptName = Path.GetFileName(scriptPath);

Console.WriteLine($"[Script Trace] Full Internal Path: {scriptPath}");
Console.WriteLine($"[Script Trace] Currently Running File Name: {scriptName}");

// Helper method leveraging the compiler directive
static string GetScriptPath([CallerFilePath] string path = "")
{
    return path;
}

while (true)
{
    Console.WriteLine();
    Console.WriteLine(new string('*', 80));
    Console.WriteLine();
    Console.WriteLine($"Current .NET Version: '{RuntimeInformation.FrameworkDescription}'");
    Console.WriteLine($"[Script Output] The persistent cache is working '{"beautifully".Humanize()}'!");
    Console.WriteLine($"[Script Output] Execution Timestamp: '{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC'");
    Thread.Sleep(5000);
    Console.WriteLine("Press the 'q' key to exit the script.");
    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
    {
        break;
    }
}
