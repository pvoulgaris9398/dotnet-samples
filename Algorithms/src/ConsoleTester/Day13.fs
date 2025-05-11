module Day13

open System.Text.RegularExpressions
open FileReader
open Common

type Button = { X: int; Y: int }
type Prize = { X: int; Y: int }

type Game =
    { ButtonA: Button
      ButtonB: Button
      Prize: Prize }

let parseInstruction str =
    let game =
        { ButtonA =
            Button {
                1
                2
            }
          ButtonB =
            Button {
                1
                2
            }
          Prize = Prize { 1, 2 } }

    game
(*
    match str with
    | ParseRegex @"(?<mul>mul)\((?<a>\d+),(?<b>\d+)\)" [ _; ToInt32 left; ToInt32 right ] -> Button(left, right)
    | ParseRegex @"(?<dont>don't)\(\)" [ _ ] -> Prize
    | _ -> failwith "Unable to parse!"
    *)

let getAllTokens (fileName: string) : (List<Game>) =
    let rx =
        Regex(@"(?<mul>mul)\((?<a>\d+),(?<b>\d+)\)|(?<dont>don't)\(\)|(?<do>do)\(\)")

    let m = rx.Matches <| allText fileName
    (*
    https://thesharperdev.com/snippets/fsharp-seq-cast/
    The `Seq.cast<Match>` usage below is key here
    *)
    m
    |> Seq.cast<Match>
    |> Seq.map (fun m -> m.Value)
    |> Seq.map (fun x -> parseInstruction x)
    |> Seq.toList

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day13.txt"

let Run =
    printfn "\nDay 13 of advent of code 2024\n"

    let allTokens = getAllTokens fileName
    allTokens |> List.iter (fun x -> printfn "%A" x)

    (*
    TODO:
    Work through the following:
    https://fsharpforfunandprofit.com/posts/recursive-types-and-folds-1b/#series-toc
    https://fsharpforfunandprofit.com/posts/conciseness-extracting-boilerplate/
    *)

    printfn "\nDay13 - DONE\n"
