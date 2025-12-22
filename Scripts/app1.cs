#!/usr/bin/env -S dotnet run

int count = 2;

var temp = Enumerable
    .Range(0, count)
    .SelectMany(x =>
        Enumerable
            .Range(0, count)
            .SelectMany(y =>
                Enumerable
                    .Range(0, count)
                    .SelectMany(z => Enumerable.Range(0, count).Select(a => (x, y, z, a)))
            )
    )
    .ToList();

foreach (var elem in temp)
{
    Console.WriteLine(elem);
}
