module ComputationExpression2

type OrElseBuilder() =
    member this.ReturnFrom(x) = x

    member this.Combine(a, b) =
        match a with
        | Some _ -> a
        | None -> b

    member this.Delay(f) = f ()

let orElse = new OrElseBuilder()

let map1 = [ ("1", "One"); ("2", "Two") ] |> Map.ofList
let map2 = [ ("A", "Alice"); ("B", "Bob") ] |> Map.ofList
let map3 = [ ("CA", "California"); ("NY", "New York") ] |> Map.ofList

let multiLookup key =
    orElse {
        return! map1.TryFind key
        return! map2.TryFind key
        return! map3.TryFind key
    }

let Run =
    multiLookup "A" |> printfn "Result for A is %A"
    multiLookup "CA" |> printfn "Result for CA is %A"
    multiLookup "X" |> printfn "Result for X is %A"
