module ActivePatterns

open System.Text.RegularExpressions
open System

//multiplicand * multiplier = product
type Instruction =
    | Multiply of product: int * int: int
    | Stop
    | Continue

let (|Integer|_|) (str: string) =
    let mutable intvalue = 0

    if System.Int32.TryParse(str, &intvalue) then
        Some(intvalue)
    else
        None

let (|ParseRegex|_|) regex str =
    let m = Regex(regex).Match(str)

    if m.Success then
        Some(List.tail [ for x in m.Groups -> x.Value ])
    else
        None

let parseInstruction str =
    match str with
    | ParseRegex @"(?<mul>mul)\((?<a>\d+),(?<b>\d+)\)" [ _; Integer left; Integer right ] -> Multiply(left, right)
    | ParseRegex @"(?<dont>don't)\(\)" [ _ ] -> Stop
    | ParseRegex @"(?<do>do)\(\)" [ _ ] -> Continue
    | _ -> failwith "Unable to parse!"

let parseDate str =
    match str with
    | ParseRegex "(\d{1,2})/(\d{1,2})/(\d{1,2})$" [ Integer m; Integer d; Integer y ] ->
        new System.DateTime(y + 2000, m, d)
    | ParseRegex "(\d{1,2})/(\d{1,2})/(\d{3,4})" [ Integer m; Integer d; Integer y ] -> new System.DateTime(y, m, d)
    | ParseRegex "(\d{1,4})-(\d{1,2})-(\d{1,2})" [ Integer y; Integer m; Integer d ] -> new System.DateTime(y, m, d)
    | _ -> new System.DateTime()

let dt1 = parseDate "12/22/08"
let dt2 = parseDate "1/1/2009"
let dt3 = parseDate "2008-1-15"
let dt4 = parseDate "1995-12-28"

printfn "%s %s %s %s" (dt1.ToString()) (dt2.ToString()) (dt3.ToString()) (dt4.ToString())

let data = [ "mul(229,919)"; "do()"; "don't()"; "mul(797,721)" ]

data |> List.map parseInstruction |> List.iter (fun x -> printfn "%A" x)

let Run = printfn "ActivePatterns"
