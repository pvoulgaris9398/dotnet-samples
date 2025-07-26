
using Algorithms;

foreach (var item in NumbersSequenceGenerator.GetNumbersSequence(10))
{
    WriteLine($"{item}");
}



var quotient = 8;

PriorityQueueExample.Run();

foreach (var i in Enumerable.Range(0, 100))
{
    WriteLine($"{nameof(i)} modulo {quotient} = {i & (quotient - 1)}");
}




