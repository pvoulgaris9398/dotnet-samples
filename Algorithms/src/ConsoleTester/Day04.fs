module Day04

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day4.txt"

(*
[<Literal>]
let testData1 =
    [ [ "A"; "B"; "C"; "D" ]
      [ "A"; "B"; "C"; "D" ]
      [ "A"; "B"; "C"; "D" ]
      [ "A"; "B"; "C"; "D" ] ]
*)
let toRows lines = lines

let toColumn (lines: seq<string>) i =
    lines
    |> Seq.map (fun row -> row[i])
    |> Seq.toArray

(*
TODO: Figure out how to make this logic work while keeping the
lines parameter a seq<string>
*)
let toColumns (lines: seq<string>) (columnCount: int) =
    let columnCount =
        match Seq.toList lines with
        | [] -> 0
        | x -> x[0].Length

    [ 0..columnCount ] |> Seq.collect (toColumn lines)

let toHorizontal verticals = ()

let getAllStrings verticals = ()

let getInputData = FileReader.lines fileName

///
/// Day 4 of: https://adventofcode.com/2024
///
let Run testing =
    printfn "\nDay04 of advent of code 2024\n"

    let data = FileReader.lines fileName

    let rows = data |> toRows

    printfn "%A" data

    let columns = data |> Seq.toList |> toColumns

    printfn "%A" columns

    printfn "\nDay04 - DONE\n"
