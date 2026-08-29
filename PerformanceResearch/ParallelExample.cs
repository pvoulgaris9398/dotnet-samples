#! /usr/bin/env -S dotnet run

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

var urls = Enumerable.Range(0, 1000).Select(i => $"https://example.com").ToList();

using var httpClient = new HttpClient();

using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

await CallApi(httpClient, cts.Token);

// 1. Configure strict concurrency limits
var options = new ParallelOptions
{
    MaxDegreeOfParallelism = 4, // Strict cap: only 4 concurrent API calls at a time
    CancellationToken = CancellationToken.None,
};

// 2. Execute the async loop
await Parallel.ForEachAsync(
    urls,
    options,
    async (url, token) =>
    {
        // --- I/O Bound Part ---
        // This yields control to the thread pool while waiting for the network
        var response = await httpClient.GetStringAsync(url, token);

        // --- CPU Bound Part ---
        // Offload heavy processing to the thread pool to keep the async loop agile
        await Task.Run(
            () =>
            {
                Console.WriteLine(
                    $"Thread Id: {Environment.CurrentManagedThreadId} - Processing data from {url}"
                );
                // Simulate heavy CPU work (e.g., parsing, data transformations, math)
                var result = ComputeHeavyMetrics(response);
                Console.WriteLine($"Processed data from {url}");
            },
            token
        );
    }
);

int ComputeHeavyMetrics(string data) => data.Length; // Placeholder CPU work

static async Task<double> ExpensiveCalculation(IReadOnlyList<double> values)
{
    double total = 0;
    object gate = new();

    Parallel.For<double>(
        0,
        values.Count,
        () => 0.0,
        (i, state, local) =>
        {
            var x = values[i];

            return local + Math.Sqrt(x) * Math.Sin(x) * Math.Log(x);
        },
        local =>
        {
            lock (gate)
            {
                total += local;
            }
        }
    );
    return total;
}

static async Task CallApi(HttpClient client, CancellationToken cancellationToken)
{
    try
    {
        var response = await client.GetAsync("/issuers/123", cancellationToken);
    }
    catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
    {
        Console.WriteLine("Operation canceled by user.");
    }
    catch (OperationCanceledException ex) when (ex.InnerException is TimeoutException)
    {
        Console.WriteLine("Operation timed out.");
    }
}
