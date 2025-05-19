module Day14

open FileReader
open Common
open System.Text.RegularExpressions

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day14.txt"

[<Literal>]
let testData1 = "p=0,4 v=3,-3"

[<Literal>]
let testData2 = $"p=0,4 v=-23,7\np=12,33 v=11,-19"

type Location = { x: int64; y: int64 }

type Velocity = { dx: int64; dy: int64 }

type Robot =
    { currentLocation: Location
      velocity: Velocity }

let parseData pattern str =
    match str with
    | ParseRegex pattern [ ToInt32 x; ToInt32 y; ToInt32 dx; ToInt32 dy ] ->
        { currentLocation = { x = x; y = y }
          velocity = { dx = dx; dy = dy } }

    | _ -> failwith "Unable to parse!"

//let newLocation xmax ymax robot : bool =
//    let newx = robot.currentLocation.x + robot.velocity.dx
//    let newy = robot.currentLocation.y + robot.velocity.dy

//    match robot with
//    | { currentLocation.x = 101 } -> false
//    | _ -> true

let getAllTokens (data: string) : List<Robot> =
    let pattern = @"p=(?<x>\d+),(?<y>\d+) v=(?<dx>-?\d+),(?<dy>-?\d+)\r\n"
    let rx = Regex(pattern)
    let m = rx.Matches <| data
    (*
    https://thesharperdev.com/snippets/fsharp-seq-cast/
    The `Seq.cast<Match>` usage below is key here
    *)
    m
    |> Seq.cast<Match>
    |> Seq.map _.Value
    |> Seq.map (fun x -> parseData pattern x)
    |> Seq.toList

///
/// Day 14 of: https://adventofcode.com/2024
///
let Run (testing: bool) =

    printfn "\nDay14 of advent of code 2024\n"

    let data = if testing then testData2 else allText fileName

    let allTokens = getAllTokens data

    if testing then
        allTokens |> List.iter (fun x -> printfn $"%A{x}")

    printfn "\nDay14 - DONE\n"
