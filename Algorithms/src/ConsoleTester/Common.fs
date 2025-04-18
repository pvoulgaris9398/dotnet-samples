module Common

open System

let (|ToInt32|_|) str =
    match Int32.TryParse (str: string) with
    | (true, int) -> Some (int)
    | _ -> None

let splitIntoArrayOfIntegers (str: string) (separator: string) (index: int) : int =
    let parts = str.Split ([| separator |], StringSplitOptions.RemoveEmptyEntries)

    match parts[index] with
    | ToInt32 element -> element
    | _ -> failwith "Unable to parse!"
