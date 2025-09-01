using Algorithms;

int[] array = [52, 96, 67, 71, 42, 38, 39, 40, 13];
var result = QuickSort.Sort(array, 0, array.Length - 1);

foreach (var item in result)
{
    WriteLine(item);
}


Functional.Test1("123");

Functional.Test1("abc");

//await Comprehensions.RunAsync().ConfigureAwait(false);

//SieveOfErastothenes.Run(100);

//foreach (var item in NumbersSequenceGenerator.GetNumbersSequence(10))
//{
//    WriteLine($"{item}");
//}

//var quotient = 8;

//PriorityQueueExample.Run();

//foreach (var i in Enumerable.Range(0, 100))
//{
//    WriteLine($"{nameof(i)} modulo {quotient} = {i & (quotient - 1)}");
//}





