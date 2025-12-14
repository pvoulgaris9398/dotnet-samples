using Xunit;

namespace Algorithms.Tests;

/// <summary>
/// General fixture for graph-related tests
/// </summary>
public sealed class GraphFixture
{
    /// <summary>
    /// Test vertex functionality
    /// </summary>
    [Fact]
    public void TestVertex()
    {
        List<Edge> edges = [];

        // Add some edges to the graph
        /*
        edges.Add(new(1, 2, 12));
        edges.Add(new(1, 3, 6));
        edges.Add(new(1, 4, 45));
        edges.Add(new(1, 5, 7));
        edges.Add(new(2, 1, 26));
        edges.Add(new(2, 4, 9));
        edges.Add(new(3, 2, 2));
        edges.Add(new(4, 3, 8));
        edges.Add(new(5, 2, 21));
        */
        Console.WriteLine($"Graph edges: {edges.Count}");
        Assert.Fail("Not implemented!");

    }
}
