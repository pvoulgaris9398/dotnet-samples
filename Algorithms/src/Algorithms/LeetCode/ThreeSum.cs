#!/usr/bin/env -S dotnet run

int[] nums = [-1, 0, 1, 2, -1, -4];

//Solution.TestNaive();

Solution.TestSortingAndTwoPointerApproach();

/*
https://leetcode.com/problems/3sum/
*/

public class Solution
{
    public static void TestNaive()
    {
        int[] nums = [-1, 0, 1, 2, -1, -4];
        var target = 0;
        var result = ThreeSumNaiveApproach(nums, target);

        foreach (var item in result.Distinct())
        {
            Console.WriteLine(item);
        }
    }

    public static void TestSortingAndTwoPointerApproach()
    {
        int[] nums = [-1, 0, 1, 2, -1, -4];
        var target = 0;
        var result = SortingAndTwoPointerApproach(nums, target);

        foreach (var item in result.Distinct())
        {
            Console.WriteLine(item);
        }
    }

    public static IEnumerable<(int, int, int)> SortingAndTwoPointerApproach(int[] nums, int target)
    {
        int len = nums.Length;
        Array.Sort(nums);

        for (int i = 0; i < len - 2; i++)
        {
            (int left, int right) = (i + 1, len - 1);

            int required = target - nums[i];

            while (left < right)
            {
                if (nums[left] + nums[right] == required)
                {
                    // TODO: Find better way
                    int[] result = [required, nums[left], nums[right]];
                    Array.Sort(result);
                    yield return (result[0], result[1], result[2]);
                    break; // <= This is key here, otherwise, we never exit this loop
                    //yield return (required, nums[left], nums[right]);
                }
                if (nums[left] + nums[right] < required)
                {
                    left++;
                }
                else if (nums[left] + nums[right] > required)
                {
                    right--;
                }
            }
        }
        yield break;
    }

    public static IEnumerable<(int, int, int)> ThreeSumNaiveApproach(int[] nums, int target)
    {
        int len = nums.Length;

        for (int i = 0; i < len - 2; i++)
        {
            for (int j = i + 1; j < len - 1; j++)
            {
                for (int k = j + 1; k < len; k++)
                {
                    if (nums[i] + nums[j] + nums[k] == target)
                    {
                        // TODO: Find better way
                        int[] result = [nums[i], nums[j], nums[k]];
                        Array.Sort(result);
                        yield return (result[0], result[1], result[2]);
                    }
                }
            }
        }
    }

    public static IList<IList<int>> ThreeSum(int[] nums)
    {
        Array.Sort(nums);
        var sorted = nums;
        var len = sorted.Length;

        List<IList<int>> results = [];

        foreach (int i in Enumerable.Range(0, len - 2))
        {
            if (i > 0 && sorted[i] == sorted[i - 1])
            {
                continue; // Skip duplicate numbers
            }

            var (left, right) = (i + 1, len - 1);

            var currentSum = sorted[i] + sorted[left] + sorted[right];

            if (currentSum == 0)
            {
                List<int> result = [sorted[i], sorted[left], sorted[right]];

                //Print(result);

                //results.Add((IList)result);

                // while (left < right && sorted[left] == sorted[left + 1])
                // {
                //     left += 1;
                // }

                // while (left < right && sorted[right] == sorted[right - 1])
                // {
                //     right -= 1;
                // }

                left += 1;
                right -= 1;
            }
            else if (currentSum < 0)
            {
                left += 1;
            }
            else
            {
                right -= 1;
            }
        }
        return results;
    }

    internal static void Print(IList<IList<int>> a)
    {
        foreach (var value in a)
        {
            foreach (var item in value)
            {
                Console.Write("{0,2}", item);
            }
        }
        Console.WriteLine();
    }
}
