namespace Algorithms;

internal sealed class UnionFind
{
    public int[] Parents { get; }
    public int[] Ranks { get; }

    public UnionFind(int size)
    {
        Parents = new int[size];
        Ranks = new int[size];
        for (int i = 0; i < size; i++)
        {
            Parents[i] = i;
            Ranks[i] = 0;
        }
    }

    /// <summary>
    /// Returns the representative item of the set containing 'item'.
    /// The "parent" of "item"
    /// </summary>
    public int Find(int item)
    {
        if (Parents[item] != item)
        {
            // Path compression optimization:
            Parents[item] = Find(Parents[item]);
        }
        return Parents[item];
    }

    /// <summary>
    /// Combines two disjoint sets containing 'item1' and 'item2'.
    /// Attaches the tree with lower rank (shorter tree)
    /// under the root of the tree with higher (taller tree) rank.
    /// </summary>
    /// <param name="item1"></param>
    /// <param name="item2"></param>
    public void Union(int item1, int item2)
    {
        int root1 = Find(item1);
        int root2 = Find(item2);
        if (root1 != root2)
        {
            if (Ranks[root1] < Ranks[root2])
            {
                Parents[root1] = root2;
            }
            else if (Ranks[root1] > Ranks[root2])
            {
                Parents[root2] = root1;
            }
            else
            {
                Parents[root2] = root1;
                Ranks[root1]++;
            }
        }
    }
}
