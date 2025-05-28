module Day01

open Common
open System

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day1.txt"

let split (on: string) (line: string) =
    let a =
        on.Split(line, StringSplitOptions.RemoveEmptyEntries ||| StringSplitOptions.TrimEntries)
        |> Seq.map (_.Trim())
        |> List.ofSeq

    a

module Tuple =
    let ofList2 =
        function
        | [ a; b ] -> (a, b)
        | _ -> failwith "Wrong-size array!"

let f2 foo (x, y) = foo x y

let f3 (a: list<int>) (b: list<int>) : list<int> * list<int> = List.sort a, List.sort b

let rec zipTwo (x: int list) (y: int list) : ((int * int) list) option =
    match x, y with
    | ([], []) -> Some []
    | (xhd :: xtl, yhd :: ytl) ->
        match zipTwo xtl ytl with
        | None -> None
        | Some lst -> Some((xhd, yhd) :: lst)
    | (_, _) -> None

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
