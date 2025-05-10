module Day02


open Common
open System

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day2.txt"

///
/// Day 2 of: https://adventofcode.com/2024
///
let Run =

    printfn "\nDay 2 of advent of code 2024\n"

    let data = FileReader.lines fileName

    let isSafe ((previous: int), (current: int)) (direction: int) =
        match current, previous, direction with
        | (current, previous, _) when Math.Abs(current - previous) < 1 -> false
        | (current, previous, _) when Math.Abs(current - previous) > 3 -> false
        | (current, previous, direction) when Math.Sign(current - previous) <> direction -> false
        | _ -> true

    let isSafe (values: List<int>) (direction: int) =
        List.pairwise values
        |> List.map (fun x -> isSafe x direction)
        |> List.forall (fun x -> x)

    let isSafe (values: List<int>) =
        let direction =
            match values with
            | [] -> None
            | x when x.Length < 2 -> None
            | _ -> Some(Math.Sign(values[1] - values[0]))

        match direction with
        | Some(int) -> isSafe values direction.Value
        | None -> false

    let exceptAt (values: List<int>) (index: int) =
        values[.. index - 1] @ values[(index + 1) ..]

    let expand (values: List<int>) : (List<List<int>>) =
        let expanded =
            values |> List.indexed |> List.map (fun (index, _) -> exceptAt values index)

        expanded

    /// Check if removing one element from the list at each index
    /// would cause the list to now be _safe_
    let checkTolerant (values: List<int>) : bool =
        let expanded = expand values
        expanded |> List.exists (fun x -> isSafe x)

    let safeCount =
        data
        |> Seq.map (fun line -> splitIntoListOfIntegers line " ")
        |> Seq.toList
        |> List.filter (fun x -> isSafe x)
        |> List.length

    let tolerantSafeCount =
        data
        |> Seq.map (fun line -> splitIntoListOfIntegers line " ")
        |> Seq.toList
        |> List.where (fun x -> checkTolerant x)
        |> List.length

    printfn "Safe Count: %i" safeCount
    printfn "Tolerant Safe Count: %i" tolerantSafeCount
