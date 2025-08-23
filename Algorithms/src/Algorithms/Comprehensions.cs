﻿namespace Algorithms
{
    internal static class Comprehensions
    {
        internal static async Task RunAsync()
        {
            /*
            var task =
                from sum in Task.Run(() => 1 + 1)
                from div in Task.Run(() => sum / 1.5)
                select sum + (int)div;
            */

            var sum = await Task.Run(() => 1 + 1).ConfigureAwait(false);

            var div = await Task.Run(() => sum / 1.5).ConfigureAwait(false);

            var result = sum + (int)div;

            Console.WriteLine(result);

        }
    }
}
