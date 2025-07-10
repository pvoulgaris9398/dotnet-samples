
var quotient = 8;

//foreach (var i in Enumerable.Range(0, 100))
//{
//    Console.WriteLine($"{nameof(i)} modulo {quotient} = {i % quotient}");
//}

foreach (var i in Enumerable.Range(0, 100))
{
    Console.WriteLine($"{nameof(i)} modulo {quotient} = {i & (quotient - 1)}");
}




