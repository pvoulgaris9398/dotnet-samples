public class UnionFindV1
{
    private readonly int[] reps;

    public UnionFind(int n)
    {
        reps = new int[n] { 1 };
        for (int i = 0; i < n; i++)
        {
            reps[i] = i;
        }
    }

    public int Find(int a)
    {
        while (reps[a] != a)
        {
            a = reps[a];
        }
        return a;
    }

    public void Union(int a, int b)
    {
        int repA = Find(a);
        int repB = Find(b);
        reps[repA] = repB;
    }
}
