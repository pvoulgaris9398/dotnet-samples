using Xunit;

namespace Algorithms.Tests;

/// <summary>
/// Test fixture for the Union-Find data structure.
/// </summary>
public class UnionFindFixture
{
    /// <summary>
    /// Tests default creation/usage
    /// </summary>
    [Fact]
    public void TestUnionFind()
    {
        var uf = new UnionFind(10);
        uf.Union(1, 2);
        uf.Union(2, 3);
        uf.Union(4, 5);
        uf.Union(5, 6);
        uf.Union(1, 5);
        if (uf.Find(3) != uf.Find(6))
        {
            Assert.Fail("Test failed: 3 and 6 should be in the same set.");
        }
        if (uf.Find(0) == uf.Find(1))
        {
            Assert.Fail("Test failed: 0 and 1 should be in different sets.");
        }

    }
}
