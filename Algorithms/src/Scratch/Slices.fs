module Slices

let exceptAt (values: List<int>) (index: int) =
    values[.. index - 1] @ values[(index + 1) ..]

let expand (values: List<int>) : (List<List<int>>) =
    let expanded =
        values |> List.indexed |> List.map (fun (index, _) -> exceptAt values index)

    expanded

let Run =
    let xs = [ 1..10 ]
    let expanded = expand xs

    printfn $"\nSlices"

module private Tests =

    /// https://fsharp.github.io/fsharp-core-docs/reference/fsharp-collections-listmodule.html#indexed
    let test (values: List<int>) : List<int * int> = List.indexed values

    let test2 (values: List<int>) : (List<int>) =
        let expanded =
            values |> List.indexed |> List.collect (fun (index, _) -> exceptAt values index)

        values |> List.append expanded

    let xs = [ 1; 2; 3; 5; 8; 13; 21; 24; 66; 78 ]

    let test3 =
        let result = exceptAt xs 5

        //result |> List.iter (fun x -> printfn "%i" x)
        ()

    let test4 =
        let result = test xs

        //result |> List.iter (fun ((x, y): int * int) -> printfn "%i: %i" x y)
        ()

    let test5 =
        let result = test2 xs
        //result |> List.iter (fun x -> printfn "%i" x)
        ()
