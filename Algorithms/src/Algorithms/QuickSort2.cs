using System.Threading.Tasks.Dataflow;

internal static class QuickSort2
{
    internal static int[] Sort(int[] array)
    {
        if (array.Length < 2) return array;

        return SortInternal(array, 0, array.Length - 1);

    }

    internal static int[] SortInternal(int[] array, int low, int high)
    {
        if (low < high)
        {
            int pivot = Partition(array, low, high);
            SortInternal(array, low, pivot - 1);
            SortInternal(array, pivot + 1, high);
        }
        return array;
    }

    internal static int Partition(int[] array, int low, int high)
    {
        int pivotValue = array[high];
        int i = low - 1;

        foreach (int j in Enumerable.Range(low, high))
        {
            if (array[j] <= pivotValue)
            {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        (array[i + 1], array[high]) = (array[high], array[i + 1]);

        return i + 1;

    }

}