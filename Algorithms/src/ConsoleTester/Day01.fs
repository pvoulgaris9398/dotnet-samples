module Day01

open Common
open System

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day1.txt"

///
/// Day 1 of: https://adventofcode.com/2024
///
let Run =

    printfn "\nDay01 of advent of code 2024\n"

    let data = FileReader.lines fileName

    let left =
        data
        |> Seq.map (fun line -> splitIntoArrayOfIntegers line " " 0)
        |> Seq.toList
        |> List.sort

    let right =
        data
        |> Seq.map (fun line -> splitIntoArrayOfIntegers line " " 1)
        |> Seq.toList
        |> List.sort

    let distance2 =
        List.zip left right |> List.map (fun (a, b) -> abs (a - b)) |> List.sum

    let distance =
        List.map2 (fun (left: int) (right: int) -> Math.Abs(left - right)) left right
        |> List.sum

    let similarity = List.filter (fun x -> List.contains x left) right |> List.sum

    printfn "Distance: %i" distance

    printfn "Distance: %i" distance2

    printfn "Similarity: %i" similarity
