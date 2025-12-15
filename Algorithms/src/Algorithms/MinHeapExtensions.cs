namespace Algorithms;

internal static class MinHeapExtensions
{
    internal static void Add(this MinHeap minHeap, int valueToAdd)
    {
        // Check if there is room:
        if (minHeap.HasRoom)
        {
            minHeap.InternalAdd(valueToAdd);
        }
    }
}
