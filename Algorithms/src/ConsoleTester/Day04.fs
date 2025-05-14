module Day04

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day4.txt"

let toRows lines = lines

let toColumns lines = lines |> List.transpose

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

    //let columns = data |> toColumns

    //printfn "%A" columns

    printfn "\nDay04 - DONE\n"
