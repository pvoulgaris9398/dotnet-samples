#!/usr/bin/env -S dotnet run

/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */

/*
https://leetcode.com/problems/add-two-numbers/
*/

using System.Numerics;

ListNode l1 = new(2, new ListNode(4, new ListNode(3)));
ListNode l2 = new(5, new ListNode(6, new ListNode(4)));

var result = Solution.AddTwoNumbers(l1, l2);
Console.WriteLine(result);

public class ListNode
{
    public int val;
    public ListNode next;

    public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
    }
}

public class Solution
{
    public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        BigInteger sum1 = CalculateSum(l1);
        Console.WriteLine("sum1 is: {0:F0}", sum1);
        BigInteger sum2 = CalculateSum(l2);
        Console.WriteLine("sum2 is: {0:F0}", sum2);

        BigInteger total = sum1 + sum2;

        ListNode newListNode = ConvertToListNode(total);

        return newListNode;
    }

    private static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    private static ListNode ConvertToListNode(BigInteger sum)
    {
        string temp = Reverse(sum.ToString("F0"));
        Console.WriteLine("Temp is {0}", temp);
        ListNode result = null;
        ListNode next = null;
        foreach (var c in temp)
        {
            if (next == null)
            {
                result = new ListNode(int.Parse(c.ToString()), null);
                next = result;
            }
            else
            {
                next.next = new ListNode(int.Parse(c.ToString()), null);
                next = next.next;
            }

            Console.WriteLine(c);
        }
        return result;
    }

    private static BigInteger CalculateSum(ListNode ln)
    {
        ListNode next = ln;
        string sumtext = string.Empty;

        while (next != null)
        {
            sumtext = next.val.ToString() + sumtext;
            //Console.WriteLine(next.val);
            next = next.next;
        }
        Console.WriteLine("Here");
        return BigInteger.Parse(sumtext);
    }
}
