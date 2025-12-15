namespace Algorithms;

internal class MinHeap
{
    private readonly int[] _data;

    private const int Arity = 2;

    public int Index { get; private set; } = 0;

    public int Size => _data.Length;

    public bool HasRoom => Size > Index;

    public MinHeap(int[] data)
    {
        _data = new int[data.Length];
        foreach (int i in Enumerable.Range(0, _data.Length))
        {
            InternalAdd(data[i]);
        }
    }

    /// <summary>
    /// Left Child = 2 * index + 1
    /// Right Child = 2 * index + 2
    /// Parent = index
    /// </summary>
    /// <param name="value"></param>
    internal void InternalAdd(int value)
    {
        _data[Index] = value;

        int idx = Index;
        while (idx > 0)
        {
            int parentIndex = (idx - 1) % 2;
            if (_data[idx] < _data[parentIndex])
            {
                (_data[parentIndex], _data[idx]) = (_data[idx], _data[parentIndex]);
            }
            idx = parentIndex;

        }
        Index++;
    }

    internal void Print()
    {
        foreach (int i in Enumerable.Range(0, Index))
        {
            WriteLine($"{i} = {_data[i]}");
        }
    }

}
