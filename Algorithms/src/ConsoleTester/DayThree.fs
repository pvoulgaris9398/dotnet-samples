module DayThree

open System.Text.RegularExpressions
open FileReader
open Common

type Instruction =
    | Multiply of product: int * int: int
    | Stop
    | Continue

let (|ParseRegex|_|) regex str =
   let m = Regex(regex).Match(str)
   if m.Success
   then Some (List.tail [ for x in m.Groups -> x.Value ])
   else None

let parseInstruction str =
    match str with
    | ParseRegex @"(?<mul>mul)\((?<a>\d+),(?<b>\d+)\)" [_; ToInt32 left; ToInt32 right] ->
        Multiply(left, right)
    | ParseRegex @"(?<dont>don't)\(\)" [_]->
        Stop
    | ParseRegex @"(?<do>do)\(\)" [_]->
        Continue
    | _ -> failwith "Unable to parse!"

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day3.txt"

let getAllTokens (fileName: string) : (List<Instruction>) =
    let rx =
        Regex (@"(?<mul>mul)\((?<a>\d+),(?<b>\d+)\)|(?<dont>don't)\(\)|(?<do>do)\(\)")

    let m = rx.Matches <| allText fileName
    (*
    https://thesharperdev.com/snippets/fsharp-seq-cast/
    The `Seq.cast<Match>` usage below is key here
    *)
    m |> Seq.cast<Match> 
    |> Seq.map (fun m -> m.Value) 
    |> Seq.map (fun x-> parseInstruction x)
    |> Seq.toList

let Run =
    printfn "\nDay 3 of advent of code 2024\n"

    let allTokens = getAllTokens fileName
    allTokens |> List.iter (fun x -> printfn "%A" x)

    (*
    TODO:
    Work through the following:
    https://fsharpforfunandprofit.com/posts/recursive-types-and-folds-1b/#series-toc
    https://fsharpforfunandprofit.com/posts/conciseness-extracting-boilerplate/
    *)

    printfn "\nDayThree - DONE\n"
