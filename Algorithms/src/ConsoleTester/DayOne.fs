module DayOne

open System
open System.Text.RegularExpressions

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day1.txt"

let (|ToInt32|_|) str =
    match Int32.TryParse (str: string) with
    | (true, int) -> Some (int)
    | _ -> None

let parseToInt str =
    match str with
    | ToInt32 i -> i
    | _ -> -1

let parseToInts str = str |> Seq.map (fun i -> parseToInt)

let splitIntoTupleOfIntegers (str: string) (separator: string) : int * int =
    let parts = str.Split (separator)

    match (parts[0], parts[1]) with
    | (ToInt32 left, ToInt32 right) -> (left, right)
    | _ -> failwith "Unable to parse!"

let (|ParseRegex|_|) regex str =
    let m = Regex(regex).Match str

    if m.Success then
        Some (List.tail [ for x in m.Groups -> x.Value ])
    else
        None

let parseIntsNoSign line =
    match line with
    | ParseRegex @"\d+" [ ToInt32 n ] -> n
    | _ -> -1

let (|Split|) (on: char) (s: string) =
    s.Split (on, StringSplitOptions.RemoveEmptyEntries ||| StringSplitOptions.TrimEntries)
    |> Array.toList

let splitLine =
    fun (line: string) -> Seq.toList (line.Split ([| " " |], StringSplitOptions.RemoveEmptyEntries))

let splitIntoArrayOfIntegers (str: string) (separator: string) (index: int) : int =
    let parts = str.Split ([| separator |], StringSplitOptions.RemoveEmptyEntries)

    match parts[index] with
    | ToInt32 element -> element
    | _ -> failwith "Unable to parse!"

let Run =

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

    //left |> Seq.iter (fun item -> printfn "%i" item)

    //right |> Seq.iter (fun item -> printfn "%i" item)

    let distance =
        List.map2 (fun (left: int) (right: int) -> Math.Abs (left - right)) left right
        |> List.sum

    let similarity = -111
    //List.filter right (fun i -> List.contains list) |> List.sum

    printfn "Distance: %i" distance

    printfn "Similarity: %i" similarity
