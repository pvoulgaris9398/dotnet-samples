
using Algorithms;

var quotient = 8;

PriorityQueueExample.Run();

foreach (var i in Enumerable.Range(0, 100))
{
    Console.WriteLine($"{nameof(i)} modulo {quotient} = {i & (quotient - 1)}");
}




