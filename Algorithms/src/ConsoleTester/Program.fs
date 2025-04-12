let f x = x * x - 2.0

let a = 10.0
let b = 20.0

let root = Bisection.Solve f a b

printfn $"Root %f{root}"

let squares =
    seq {
        for i in 1..100 do
            yield i * i
    }

for sq in squares do
    printfn $"%d{sq}"

open Plotly.NET
Covariance.sampleChart |> Chart.show
