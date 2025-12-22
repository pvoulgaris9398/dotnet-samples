#!/usr/bin/env -S dotnet run

Test3();

static void Test3()
{
    int[] a = [1, 2, 3, 4, 5];
    int[] b = [6, 7, 8];

    Console.WriteLine(MergeTwoSortedArrays.MedianOf2(a, b));
}

static void Test2()
{
    int[] a = [-5, 3, 6, 12, 15];
    int[] b = [-12, -10, -6, -3, 4, 10];

    Console.WriteLine(MergeTwoSortedArrays.MedianOf2(a, b));
}

static void Test1()
{
    int[] leftArray = [1, 4, 6, 12];
    int[] rightArray = [2, 5, 7, 17];

    Console.WriteLine("{0}: {1}", nameof(leftArray), string.Join(',', leftArray));
    Console.WriteLine("{0}: {1}", nameof(rightArray), string.Join(',', rightArray));

    int[] merged = MergeTwoSortedArrays.Merge(leftArray, rightArray);

    Console.WriteLine("{0}: {1}", nameof(merged), string.Join(',', merged));

    Console.WriteLine("Median is {0}", MergeTwoSortedArrays.Median(merged));
}

internal static class MergeTwoSortedArrays
{

    internal static double MedianOf2(int[] left, int[] right)
    {
        int n = left.Length;
        int m = right.Length;

        if (n > m)
        {
            return MedianOf2(right, left);
        }

        int low = 0;
        int high = n;

        while (low <= high)
        {

            int mid1 = (low + high) / 2;
            int mid2 = (n + m + 1) / 2 - mid1;

            // Elements to the left and right of partition in left[]
            int l1 = (mid1 == 0 ? int.MinValue : left[mid1 - 1]);
            int r1 = (mid1 == n ? int.MaxValue : left[mid1]);

            // Elements to the left and right of partition in right[]
            int l2 = (mid2 == 0 ? int.MinValue : right[mid2 - 1]);
            int r2 = (mid2 == m ? int.MaxValue : right[mid2]);

            // If it is a valid partition
            if (l1 <= r2 && l2 <= r1)
            {
                // if the total elements are even, then median is
                // the average of the two middle elements
                if ((n + m) % 2 == 0)
                {
                    return (Math.Max(l1, l2) + Math.Min(r1, r2)) / 2.0;
                }
                else
                {
                    return Math.Max(l1, l2);
                }
            }

            // Check if need to take less elements from arr1
            if (l1 > r2)
            {
                high = mid1 - 1;
            }
            // Check if need to take more elements from arr1
            else
            {
                low = mid1 + 1;
            }
        }
        return 0;
    }
    internal static double Median(int[] array)
    {
        int arrayLength = array.Length;

        if (arrayLength < 1) return -1;

        if (arrayLength % 2 == 0)
        {
            int n1 = array[(arrayLength / 2) - 1];
            int n2 = array[arrayLength / 2];
            return (n1 + n2) / 2.0d;
        }
        else
        {
            return array[arrayLength / 2];
        }

    }
    internal static int[] Merge(int[] leftArray, int[] rightArray)
    {
        int[] result = new int[leftArray.Length + rightArray.Length];

        int i = 0;
        int j = 0;
        int k = 0;

        while (i < leftArray.Length && j < rightArray.Length)
        {

            Console.WriteLine(new string('*', 80));

            if (leftArray[i] <= rightArray[j])
            {
                Console.WriteLine("Before: {0}: {1}", nameof(result), string.Join(',', result));
                result[k++] = leftArray[i++];
                Console.WriteLine("After: {0}: {1}", nameof(result), string.Join(',', result));
                Console.WriteLine("{0}: {1}", nameof(leftArray), string.Join(',', leftArray[i..]));
            }
            else
            {
                Console.WriteLine("Before: {0}: {1}", nameof(result), string.Join(',', result));
                result[k++] = rightArray[j++];
                Console.WriteLine("After: {0}: {1}", nameof(result), string.Join(',', result));
                Console.WriteLine("{0}: {1}", nameof(rightArray), string.Join(',', rightArray[j..]));
            }
        }

        while (i < leftArray.Length)
        {
            Console.WriteLine("Before: {0}: {1}", nameof(result), string.Join(',', result));
            result[k++] = leftArray[i++];
            Console.WriteLine("After: {0}: {1}", nameof(result), string.Join(',', result));
            Console.WriteLine("{0}: {1}", nameof(leftArray), string.Join(',', leftArray[i..]));
        }

        while (j < rightArray.Length)
        {
            Console.WriteLine("Before: {0}: {1}", nameof(result), string.Join(',', result));
            result[k++] = rightArray[j++];
            Console.WriteLine("After: {0}: {1}", nameof(result), string.Join(',', result));
            Console.WriteLine("{0}: {1}", nameof(rightArray), string.Join(',', rightArray[j..]));
        }

        return result;

    }
}