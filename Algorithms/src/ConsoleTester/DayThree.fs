module DayThree

open System.Text.RegularExpressions
open FileReader

type Instruction = interface end

type Multiply (left: int, right: int) =
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

let getAllTokens (fileName: string): (List<string>)=
    let rx = Regex(@"(?<mul>mul)\((?<a>\d+),(?<b>\d+)\)|(?<dont>don't)\(\)|(?<do>do)\(\)")
    let m = rx.Matches <| allText fileName
    (*
    https://thesharperdev.com/snippets/fsharp-seq-cast/
    The `Seq.cast<Match>` usage below is key here
    *)
    m |> Seq.cast<Match> |> Seq.map(fun m -> m.Value) |> Seq.toList    

let Run =
    printfn "\nDay 3 of advent of code 2024\n"

    let allTokens = getAllTokens fileName
    allTokens |> List.iter(fun x -> printfn "%s" x)

    printfn "\nDayThree - DONE\n"

