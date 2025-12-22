#!/usr/bin/env -S dotnet run

QuickSort.RunWith([1, 101, 8, 4, 18, 12, 6, 33, 25, 9, 30, 2, 7, 3]);

QuickSort.RunWith([1, 11, 5, 22, 3]);

internal static class QuickSort
{
    internal static void RunWith(int[] array)
    {
        Console.WriteLine(new string('*', 80));

        Console.WriteLine("Original Array: {0}", string.Join(',', array));

        int[] sorted = QuickSort.Sort(array, 0, array.Length - 1);

        Console.WriteLine("Sorted Array: {0}", string.Join(',', sorted));

        Console.WriteLine("Median: {0}", QuickSort.Median(sorted));
    }

    internal static double Median(int[] array)
    {
        int len = array.Length;
        if (len % 2 == 0)
        {
            return (array[len / 2] + array[(len / 2) - 1]) / 2.0;
        }
        else
        {
            return array[len / 2];
        }
    }

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
        return [];
    }
}
