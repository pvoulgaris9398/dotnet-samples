#!/usr/bin/env -S dotnet run

var result1 = new Solution().TwoSum([2, 7, 11, 15], 9);
Console.WriteLine("({0}, {1})", result1[0], result1[1]);

var result2 = new Solution().TwoSum([3, 2, 4], 6);

Console.WriteLine("({0}, {1})", result2[0], result2[1]);

/*
https://leetcode.com/problems/two-sum/
*/
public class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        var state = new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            var candidate = target - nums[i];

            if (candidate < 0)
                continue;

            if (state.TryGetValue(candidate, out int index1))
            {
                return [index1, i];
            }
            else
            {
                state[nums[i]] = i;
            }
        }
        return [-1, -1];
    }
}
