#!/usr/bin/env -S dotnet run

/*
443. String Compression
*/

Console.WriteLine($"{Solution.Compress(['a', 'a', 'b', 'b', 'c', 'c', 'c'])}");

Console.WriteLine($"{Solution.Compress(['a', 'a', 'b', 'b', 'c', 'c', 'd'])}");

Console.WriteLine($"{Solution.Compress(['a'])}");

public class Solution
{

    public static int Compress(char[] chars)
    {
        int reader = 0; // Pointer 1 (read pointer)
        int lookAhead = 0; // Pointer 2 (scouts ahead)
        int n = chars.Length;
        int writer = 0; // Pointer 3 (write pointer)

        while (reader < n)
        {
            lookAhead = reader + 1;
            while (lookAhead < n && chars[lookAhead] == chars[reader])
            {
                lookAhead += 1;
            }
            chars[writer] = chars[reader];
            writer += 1;
            if ((lookAhead - reader) > 1)
            {
                var digits = (lookAhead - reader).ToString();
                for (int i = 0; i < digits.Length; i++)
                {
                    chars[writer] = digits[i];
                    writer += 1;
                }
            }
            reader = lookAhead;
        }
        return writer;
    }

    public static int Compress_Number2(char[] chars)
    {
        int outputPointer = 0;
        int p1 = 0;
        int p2 = 0;

        while (p2 < chars.Length)
        {
            if (p2 > chars.Length - 1 || chars[p1] != chars[p2])
            {
                // Output the count  
                if ((p2 - p1) > 1)
                {
                    // Output the count
                    // If charCount == 0 we are done, no need to output the zero, nor reset it
                    var charCount = (p2 - p1);
                    if (charCount > 1)
                    {
                        // Convert digit to string and start chomping it from the left
                        var digits = charCount.ToString();
                        for (int j = 0; j < digits.Length; j++)
                        {
                            chars[outputPointer] = digits[j];
                            outputPointer += 1;
                        }
                    }
                    outputPointer += 1;
                }
                if (p2 > chars.Length) break; // Exit loop

                p1 = p2;

            }
            p2 += 1;
        }

        return outputPointer;
    }

    public static int Compress_Number1(char[] chars)
    {
        /*
        By problem statement/constraints there is at
        least one element and no more than 2,000 in chars array
        Return it, since subsequent logic depends on "look-ahead"
        functionality to work correctly
        */

        char currentChar = chars[0];
        int outputPointer = 0;
        int charCount = 1;

        // Since we are looking for i+1 below, make sure there is at least one last element
        for (int i = 0; i < chars.Length - 1; i++)
        {
            // Making this i + 1 made this work
            if (currentChar != chars[i + 1])
            {
                // Output the current char
                chars[outputPointer] = currentChar;

                // Increment the output pointer
                outputPointer += 1;

                // Reset the current to the new value
                currentChar = chars[i];

                // Output the count
                // If charCount == 0 we are done, no need to output the zero, nor reset it
                if (charCount > 1)
                {
                    // Convert digit to string and start chomping it from the left
                    var digits = charCount.ToString();
                    for (int j = 0; j < digits.Length; j++)
                    {
                        chars[outputPointer] = digits[j];
                        outputPointer += 1;
                    }
                    // Now reset char counter
                    charCount = 1;
                }
            }
            else
            {
                charCount += 1;
            }
        }

        chars[outputPointer] = currentChar;
        outputPointer += 1;

        if (charCount > 1)
        {
            // Convert digit to string and start chomping it from the left
            var digits = charCount.ToString();
            for (int j = 0; j < digits.Length; j++)
            {
                chars[outputPointer] = digits[j];
                outputPointer += 1;
            }
            // Now reset char counter
            charCount = 1;
        }

        return outputPointer;
    }
}