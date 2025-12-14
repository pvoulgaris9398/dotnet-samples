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

    public int Find(int item)
    {
        if (Parents[item] != item)
        {
            Parents[item] = Find(Parents[item]);
        }
        return Parents[item];
    }

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
