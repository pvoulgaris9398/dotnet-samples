module Common

open System.Text.RegularExpressions
open System

let (|ParseRegex|_|) regex str =
    let m = Regex(regex).Match(str)

    if m.Success then
        Some(List.tail [ for x in m.Groups -> x.Value ])
    else
        None

let (|ToInt32|_|) str =
    match Int32.TryParse(str: string) with
    | (true, int) -> Some(int)
    | _ -> None

let splitIntoListOfIntegers (str: string) (separator: string) : List<int> =
    str.Split([| separator |], StringSplitOptions.RemoveEmptyEntries)
    |> Array.map (fun x -> Int32.Parse x)
    |> Array.toList

let splitIntoArrayOfIntegers (str: string) (separator: string) (index: int) : int =
    let parts = str.Split([| separator |], StringSplitOptions.RemoveEmptyEntries)

    match parts[index] with
    | ToInt32 element -> element
    | _ -> failwith "Unable to parse!"
