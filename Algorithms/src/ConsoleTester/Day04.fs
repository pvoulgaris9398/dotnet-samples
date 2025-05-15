module Day04

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day4.txt"


let testData1: seq<string> =
    seq {
        yield "ABCD"
        yield "ABCD"
        yield "ABCD"
        yield "ABCD"
    }


let toRows lines = lines

let toColumn (lines: seq<string>) i =
    lines |> Seq.map (fun row -> string row[i])

let toColumns (lines: seq<string>) =
    let columnCount =
        match Seq.toList lines with
        | [] -> 0
        | x -> x[0].Length - 1

    [ 0..columnCount ] |> Seq.collect (toColumn lines)

let toHorizontal verticals = ()

let getAllStrings verticals = ()

let getInputData = FileReader.lines fileName

///
/// Day 4 of: https://adventofcode.com/2024
///
let Run (testing: bool) =
    printfn "\nDay04 of advent of code 2024\n"

    let data =
        match testing with
        | true -> testData1
        | false -> getInputData

    let rows = data |> toRows

    printfn "%A" rows

    let columns = data |> Seq.toList |> toColumns |> Seq.toList

    printfn "%A" columns

    printfn "\nDay04 - DONE\n"
