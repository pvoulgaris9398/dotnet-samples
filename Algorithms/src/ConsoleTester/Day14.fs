module Day14

open FileReader
open Common
open System.Text.RegularExpressions

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day14.txt"

[<Literal>]
let testData1 = "p=0,4 v=3,-3"

type Location = { x: int64; y: int64 }

type Velocity = { dx: int64; dy: int64 }

type Robot =
    { currentLocation: Location
      velocity: Velocity }

type DataPoint =
    | Point of x: int64 * y: int64
    | Velocity of dx: int64 * dy: int64

(*
TODO: Properly handle the negative sign and
combine these types into one type
*)
let parseInstruction str =
    match str with
    | ParseRegex @"(?<point>p=)(?<x>\d+),(?<y>\d+)" [ _; ToInt32 x; ToInt32 y ] -> Point(x, y)
    | ParseRegex @"(?<velocity>v=(?<dx>^-?\d+$),(?<dy>^-?\d+$))" [ _; ToInt32 dx; ToInt32 dy ] -> Velocity(dx, dy)
    | _ -> failwith "Unable to parse!"

let parseInstruction2 str =
    match str with
    //@"(?<point>p=)(?<x>\d+),(?<y>\d+) (?<velocity>v=(?<dx>^-?\d+),(?<dy>^-?\d+))"
    | ParseRegex @"(?p=(?<x>\d+),(?<y>\d+) v=(?<dx>^-?\d+),(?<dy>^-?\d+)" [ ToInt32 x; ToInt32 y; ToInt32 dx; ToInt32 dy ] ->
        //Robot(currentLocation (x, y), velocity (dx, dy))


        { currentLocation = { x = x; y = y }
          velocity = { dx = dx; dy = dy } }

    | _ -> failwith "Unable to parse!"

let getAllTokens (fileName: string) : List<Robot> =
    let rx =
        Regex(@"(?<point>p=)(?<x>\d+),(?<y>\d+) (?<velocity>v=(?<dx>^-?\d+),(?<dy>^-?\d+))")

    let m = rx.Matches <| testData1 //allText fileName
    (*
    https://thesharperdev.com/snippets/fsharp-seq-cast/
    The `Seq.cast<Match>` usage below is key here
    *)
    m
    |> Seq.cast<Match>
    |> Seq.map _.Value
    |> Seq.map parseInstruction2
    |> Seq.toList

///
/// Day 14 of: https://adventofcode.com/2024
///
let Run (testing: bool) =

    printfn "\nDay14 of advent of code 2024\n"

    let allTokens = getAllTokens fileName

    if testing then
        allTokens |> List.iter (fun x -> printfn $"%A{x}")

    printfn "\nDay14 - DONE\n"
