using Xunit;
using Xunit.Abstractions;

namespace Algorithms.Tests;

/// <summary>
/// General fixture for graph-related tests
/// </summary>
public sealed class GraphFixture(ITestOutputHelper output)
{

    private readonly ITestOutputHelper _output = output;


    /// <summary>
    /// Test vertex functionality
    /// </summary>
    [Fact]
    public void TestVertex()
    {
        List<Edge> edges = [];

        Vertex a = new("a");
        Vertex b = new("b");
        Vertex c = new("c");
        Vertex d = new("d");
        Vertex e = new("e");

        // Add some edges to the graph

        edges.Add(new(a, b, 12));
        edges.Add(new(a, c, 6));
        edges.Add(new(a, d, 45));
        edges.Add(new(a, e, 7));
        edges.Add(new(b, a, 26));
        edges.Add(new(b, d, 9));
        edges.Add(new(c, b, 2));
        edges.Add(new(d, c, 8));
        edges.Add(new(e, b, 21));

        foreach (Edge edge in edges)
        {
            _output.WriteLine($"Graph edges: {edge}");
        }

        a.Traverse(edges, edge => _output.WriteLine($"Visited vertex: {edge.Id}"));

    }
}
