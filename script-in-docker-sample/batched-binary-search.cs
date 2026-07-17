#!/usr/bin/env -S dotnet run

Console.WriteLine(CountFound([1, 3, 5, 7], [3, 4, 7]));
Console.WriteLine(CountFound([2, 4, 6], [1, 3, 5]));
Console.WriteLine(CountFound([1, 2, 3], [1, 2, 3]));
Console.WriteLine(CountFound([1, 2], []));
Console.WriteLine(CountFound([5], [5, 5]));

static int CountFound(int[] nums, int[] queries)
{
    int found = 0;

    foreach (int q in queries)
    {
        int lo = 0;
        int hi = nums.Length - 1;

        while (lo <= hi)
        {
            int mid = lo + ((hi - lo) / 2);

            if (nums[mid] == q)
            {
                found++;
                break;
            }
            if (nums[mid] > q)
            {
                hi = mid - 1;
            }
            if (nums[mid] < q)
            {
                lo = mid + 1;
            }
        }
    }
    return found;
}
