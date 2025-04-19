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

    let isSafe ((previous: int), (current: int)) (direction: int) =
        match current, previous, direction with
        | (current, previous, _) when Math.Abs (current - previous) < 1 -> false
        | (current, previous, _) when Math.Abs (current - previous) > 3 -> false
        | (current, previous, direction) when Math.Sign (current - previous) <> direction -> false
        | _ -> true

    let isSafe (values: List<int>) (direction: int) =
        List.pairwise values |> List.map (fun x -> isSafe x direction)

    let isSafe (values: List<int>) =
        let direction =
            match values with
            | [] -> None
            | x when x.Length < 2 -> None
            | _ -> Some (values[1] - values[0])

        match direction with
        | Some (int) -> isSafe values direction.Value
        | None -> []

    let safeCount =
        data
        |> Seq.map (fun line -> splitIntoArrayOfIntegers line " ")
        |> Seq.toList
        |> List.length

    ()
