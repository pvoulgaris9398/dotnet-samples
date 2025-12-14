
internal static class QuickSort
{
    internal static int[] Sort(int[] array, int leftIndex, int rightIndex)
    {
        int i = leftIndex;
        int j = rightIndex;
        int pivot = array[leftIndex];

        while (i < j)
        {
            while (array[i] < pivot)
            {
                i++;
            }

            while (array[j] > pivot)
            {
                j--;
            }

            if (i <= j)
            {
                (array[j], array[i]) = (array[i], array[j]);
                i++;
                j--;
            }
        }

        if (leftIndex < j)
        {
            _ = Sort(array, leftIndex, j);
        }

        if (i < rightIndex)
        {
            _ = Sort(array, i, rightIndex);
        }

        return array;
    }
}
