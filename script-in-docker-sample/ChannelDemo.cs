#!/usr/bin/env -S dotnet run

// ChannelDemo.cs
// Run with: dotnet run ChannelDemo.cs   (or paste into a new console project)
//
// Simulates several background "producers" generating rapid updates,
// and a single "consumer" that drains + batches them before pushing
// to something UI-thread-like (here, just Console.WriteLine to keep it self-contained).

using System.Threading.Channels;

var channel = Channel.CreateBounded<UpdateEvent>(new BoundedChannelOptions(capacity: 50)
{
    FullMode = BoundedChannelFullMode.DropOldest, // if the consumer falls behind, drop stale updates
    SingleReader = true,                          // only one consumer loop below
    SingleWriter = false                          // multiple producers write concurrently
});

var cts = new CancellationTokenSource();

// ---- Consumer: single loop, batches bursts before "publishing" ----
var consumerTask = Task.Run(async () =>
{
    var batch = new List<UpdateEvent>();

    await foreach (var update in channel.Reader.ReadAllAsync(cts.Token))
    {
        batch.Add(update);

        // Drain anything else already sitting in the channel (non-blocking)
        // so a burst becomes one batch instead of many individual "UI" calls.
        while (channel.Reader.TryRead(out var more))
            batch.Add(more);

        PublishToUi(batch);
        batch.Clear();

        await Task.Delay(50); // simulate a UI refresh cap (~20 updates/sec)
    }
});

// ---- Producers: simulate independent background sources ----
var producerTasks = Enumerable.Range(1, 3).Select(id => Task.Run(async () =>
{
    var rng = new Random(id);
    for (int i = 0; i < 20; i++)
    {
        var evt = new UpdateEvent(Source: $"Producer-{id}", Value: rng.Next(0, 100), Timestamp: DateTime.Now);

        // TryWrite is non-blocking; with DropOldest it never awaits/blocks the producer
        _ = channel.Writer.TryWrite(evt);

        await Task.Delay(rng.Next(5, 30)); // producers fire faster than the UI consumes
    }
})).ToArray();

await Task.WhenAll(producerTasks);
channel.Writer.Complete();   // signals the consumer's ReadAllAsync loop to end
await consumerTask;

Console.WriteLine("Done.");

static void PublishToUi(List<UpdateEvent> batch)
{
    // In a real app, this body runs on the UI thread, e.g.:
    //   dispatcherQueue.TryEnqueue(() => ApplyBatchToUi(batch));
    // or for Blazor: await InvokeAsync(() => { Apply(batch); StateHasChanged(); });
    Console.WriteLine($"[UI] Publishing batch of {batch.Count} update(s): " +
        string.Join(", ", batch.Select(b => $"{b.Source}={b.Value}")));
}

internal record UpdateEvent(string Source, int Value, DateTime Timestamp);