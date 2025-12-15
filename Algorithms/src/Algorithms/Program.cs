using Algorithms;

WriteLine(new string('*', 80));

WriteLine("MinHeap Test");

var minHeap = new MinHeap([10, 3, 2, 4, 5, 1]);

minHeap.Print();

WriteLine(new string('*', 80));

WriteLine("QuickSort Test 1");

int[] array1 = [52, 96, 67, 71, 42, 38, 39, 40, 13];
int[] result1 = QuickSort.Sort(array1, 0, array1.Length - 1);

foreach (int item in result1)
{
    WriteLine(item);
}

WriteLine("QuickSort2 Test 2");

int[] array2 = [5, 4, 3, 5, 7, 6, 9, 4, 1, 1, 3, 4, 50, 56, 3, 41, 3];
int[] result2 = QuickSort2.Sort(array2);

foreach (int item in result2)
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





