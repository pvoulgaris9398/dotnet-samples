using Algorithms;

WriteLine("QuickSort Test 1");

int[] array1 = [52, 96, 67, 71, 42, 38, 39, 40, 13];
var result1 = QuickSort.Sort(array1, 0, array1.Length - 1);

foreach (var item in result1)
{
    WriteLine(item);
}

WriteLine("QuickSort Test 2");

int[] array2 = [5, 4, 3, 5, 7, 6, 9, 4, 1, 1, 3, 4, 50, 56, 3, 41, 3];
var result2 = QuickSort.Sort(array2, 0, array2.Length - 1);

foreach (var item in result2)
{
    WriteLine(item);
}

Environment.Exit(0);

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





