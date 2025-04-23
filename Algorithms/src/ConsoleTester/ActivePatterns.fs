module ActivePatterns

open System.Text.RegularExpressions

//type Instruction
//    |Multiply
//    |Stop
//    |Continue

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
//        Some (m.Groups.["a"].Value, m.Groups.["b"].Value)
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

let Run = printfn "ActivePatterns"
