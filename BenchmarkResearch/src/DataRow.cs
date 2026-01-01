#pragma warning disable IDE0160 // Convert to block scoped namespace
namespace BenchmarkResearch;

#pragma warning restore IDE0160 // Convert to block scoped namespace
public sealed record DataRow(DateOnly Date, decimal Close, string Name);
