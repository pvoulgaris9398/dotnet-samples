
internal static class QuickSort
{
    internal static int[] Sort(int[] array, int leftIndex, int rightIndex)
    {
        var i = leftIndex;
        var j = rightIndex;
        var pivot = array[leftIndex];

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
            Sort(array, leftIndex, j);
        }

        if (i < rightIndex)
        {
            Sort(array, i, rightIndex);
        }

        return array;
    }
}
