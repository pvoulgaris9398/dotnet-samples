module DayThree

open System.Text.RegularExpressions
open FileReader

type Instruction = interface end

type Multiply(left: int, right: int) =
    class
        interface Instruction
    end

    member this.Left = left
    member this.Right = right

type Stop =
    class
        interface Instruction
    end

type Continue =
    class
        interface Instruction
    end

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day3.txt"

let getAllTokens (fileName: string) : (List<string>) =
    let rx =
        Regex (@"(?<mul>mul)\((?<a>\d+),(?<b>\d+)\)|(?<dont>don't)\(\)|(?<do>do)\(\)")

    let m = rx.Matches <| allText fileName
    (*
    https://thesharperdev.com/snippets/fsharp-seq-cast/
    The `Seq.cast<Match>` usage below is key here
    *)
    m |> Seq.cast<Match> |> Seq.map (fun m -> m.Value) |> Seq.toList

let (|ToMultiply|_|) (input: string) ((a: int), (b: int)) =
    let m = Regex.Match (input, @"(?<mul>mul)\((?<a>\d+),(?<b>\d+)\)")

    if (m.Success) then
        Some (m.Groups.["a"].Value, m.Groups.["b"].Value)
    else
        None



//let (|Multiply|Stop|Continue|None|)  (input:string) =
//   let toMultiply = Regex ""
//   let toStop = Regex ""
//   let toContinue = Regex ""
//   fun input ->
//       if toMultiply.IsMatch input then Multiply
//       elif toStop.IsMatch input the Stop
//       elif toContinue.IsMatch input then Continue
//       else None

let Run =
    printfn "\nDay 3 of advent of code 2024\n"

    let allTokens = getAllTokens fileName
    allTokens |> List.iter (fun x -> printfn "%s" x)

    printfn "\nDayThree - DONE\n"
