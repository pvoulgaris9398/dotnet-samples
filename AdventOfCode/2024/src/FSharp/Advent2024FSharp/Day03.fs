module Day03

open System.Text.RegularExpressions
open FileReader
open Common

type Instruction =
    | Multiply of product: int64 * int: int64
    | Stop
    | Continue

let parseInstruction str =
    match str with
    | ParseRegex @"(?<mul>mul)\((?<a>\d+),(?<b>\d+)\)" [ _; ToInt32 left; ToInt32 right ] -> Multiply(left, right)
    | ParseRegex @"(?<dont>don't)\(\)" [ _ ] -> Stop
    | ParseRegex @"(?<do>do)\(\)" [ _ ] -> Continue
    | _ -> failwith "Unable to parse!"

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day3.txt"

let getAllTokens (fileName: string) : List<Instruction> =
    let rx =
        Regex(@"(?<mul>mul)\((?<a>\d+),(?<b>\d+)\)|(?<dont>don't)\(\)|(?<do>do)\(\)")

    let m = rx.Matches <| allText fileName
    (*
    https://thesharperdev.com/snippets/fsharp-seq-cast/
    The `Seq.cast<Match>` usage below is key here
    *)
    m
    |> Seq.cast<Match>
    |> Seq.map _.Value
    |> Seq.map parseInstruction
    |> Seq.toList

let calculate1 (total: int64) instruction =
    match instruction with
    | Multiply(x, y) -> (total + x * y)
    | _ -> total

let calculate2 (total: int64, includeNext: bool) instruction =
    match instruction with
    | Multiply(x, y) ->
        match includeNext with
        | true -> (total + x * y, includeNext)
        | false -> (total, includeNext)
    | Stop -> (total, false)
    | Continue -> (total, true)

///
/// Day 3 of: https://adventofcode.com/2024
///
let Run testing =
    printfn "\nDay03 of advent of code 2024\n"

    let allTokens = getAllTokens fileName

    if testing then
        allTokens |> List.iter (fun x -> printfn $"%A{x}")

    let sum1 = allTokens |> List.fold calculate1 0L

    printfn $"The sum is: %i{sum1}"

    let sum2, _ = allTokens |> List.fold calculate2 (0L, true)

    printfn $"The sum is: %i{sum2}"

    (*
    TODO:
    Work through the following:
    https://fsharpforfunandprofit.com/posts/conciseness-extracting-boilerplate/
    https://dev.to/shimmer/series/4422
    *)

    printfn "\nDay03 - DONE\n"
