#!/usr/bin/env -S dotnet run --property:AllowUnsafeBlocks=true

/*
Taking a look at using SIMD (single-instruction, multiple data) capabilities in dotnet
https://medium.com/@anderson.buenogod/accelerating-financial-calculations-with-avx-512-in-c-net-8-5c542fd59958
*/

using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

if (args.Length > 0)
{
    Action actionToExecute = args[0] switch
    {
        "test0" => Tests.Test0,
        //"test2" => Tests.Test2,
        _ => () => Console.WriteLine("Unrecognized command-line argument"),
    };
    actionToExecute();
    return;
}

internal static class Tests
{
#pragma warning disable CS0227
    internal static unsafe void Test0()
    {
        double[] initialValues = [1000.0, 1500.0, 2000.0, 2500.0, 3000.0, 3500.0, 4000.0, 5000.0];

        double[] interestRates = [1.05, 1.04, 1.06, 1.3, 1.05, 1.07, 1.06, 1.03];

        int years = 10;

        double[] finalValues = new double[8];

        Vector512<double> investments;
        fixed (double* p1 = initialValues)
        {
            investments = Avx512F.LoadVector512(p1);
        }

        Vector512<double> rates;
        fixed (double* p2 = interestRates)
        {
            rates = Avx512F.LoadVector512(p2);
        }

        var compoundedRate = Calculations.PowAvx512(rates, years);

        var futureValues = Avx512F.Multiply(investments, compoundedRate);

        fixed (double* p2 = finalValues)
        {
            Avx512F.Store(p2, futureValues);
        }

        foreach (var value in finalValues)
        {
            Console.WriteLine($"{value:C}");
        }
    }
#pragma warning restore CS0227
}

internal static class Calculations
{
    internal static Vector512<double> PowAvx512(Vector512<double> vector, int exponent)
    {
        var result = vector;
        for (int i = 1; i < exponent; i++)
        {
            result = Avx512F.Multiply(result, vector);
        }
        return result;
    }
}
