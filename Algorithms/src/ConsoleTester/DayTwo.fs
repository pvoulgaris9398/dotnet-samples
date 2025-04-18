module DayTwo

open Common
open System

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day2.txt"

///
/// Day 2 of: https://adventofcode.com/2024
///
let Run =

    printfn "Day 2 of advent of code 2024\n"

    let data = FileReader.lines fileName

    let isSafe (values: List<string>) =
        match values with
        | [] -> false
        | x when x.Length < 2 -> true
        | _ -> false

    let isSafe (values: List<string>) (direction: int) = true

    let isSafe (previous: int) (current: int) (direction: int) =
        match current, previous, direction with
        | (current, previous, _) when Math.Abs (current - previous) < 1 -> false
        | (current, previous, _) when Math.Abs (current - previous) > 3 -> false
        | (current, previous, direction) when Math.Sign (current - previous) <> direction -> false
        | _ -> true

    let allLists = List.map (fun line -> splitIntoArrayOfIntegers line " ")


    ()
