module ActivePatterns

open System.Text.RegularExpressions
open Common

//multiplicand * multiplier = product
type Instruction =
    | Multiply of product: float * float: float
    | Stop
    | Continue

//type Stop =
//    class
//        interface Instruction
//    end

//type Continue =
//    class
//        interface Instruction
//    end

//let (|ToMultiply|) (input: string) ((a: int), (b: int)) =
//    let m = Regex.Match (input, @"(?<mul>mul)\((?<a>\d+),(?<b>\d+)\)")

//    if (m.Success) then
//        Multiply (ToInt32 m.Groups.["a"].Value ToInt32 m.Groups.["b"].Value)
//    else
//        None

//let test1 (str: string) : (Instruction) =
//    match str with
//    | ToMultiply str -> Multiply a b
//    | _ -> failwith "Error!"

//let getAllTokens2 (fileName: string) : (List<Instruction>) =
//    let rx =
//        Regex (@"(?<mul>mul)\((?<a>\d+),(?<b>\d+)\)|(?<dont>don't)\(\)|(?<do>do)\(\)")

//    (*
//    https://thesharperdev.com/snippets/fsharp-seq-cast/
//    The `Seq.cast<Match>` usage below is key here
//    *)
//    let m = rx.Matches <| allText fileName

//    //m |> Seq.cast<Match> |> Seq.toList |> List.map (fun x -> x.Value >)
//    []

l

let Run = printfn "ActivePatterns"
