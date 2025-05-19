module Day14

open FileReader
open Common
open System.Text.RegularExpressions

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day14.txt"

[<Literal>]
let testData1 = "p=2,4 v=2,-3\r\n"

[<Literal>]
let testData2 = $"p=0,4 v=-23,7p=12,33 v=11,-19\r\n"

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

let newLocation xmax ymax robot : Robot =
    let deltax = robot.currentLocation.x + robot.velocity.dx
    let deltay = robot.currentLocation.y + robot.velocity.dy

    let newx =
        match deltax with
        | x when deltax < 0 -> xmax + deltax
        | x when deltax >= 0 -> deltax
        | _ -> deltax

    let newy =
        match deltay with
        | x when deltay < 0 -> ymax + deltay
        | x when deltay >= 0 -> deltay
        | _ -> deltay

    { currentLocation = { x = newx; y = newy }
      velocity = robot.velocity }

let moveOneFor (newLocation: Robot -> Robot) (iterations: int) (robot: Robot) : Robot =
    [ 1..iterations ] |> Seq.fold (fun x _ -> newLocation x) robot

let moveAllFor (moveOneFor: Robot -> Robot) (robots: seq<Robot>) = robots |> Seq.map moveOneFor

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

    let newLocationConfigured = newLocation 101 103
    let moveOneForConfigured = moveOneFor newLocationConfigured 100

    let locations =
        allTokens |> List.toSeq |> moveAllFor (fun x -> moveOneForConfigured x)

    locations |> Seq.iter (fun x -> printfn $"%A{x}")

    (*
    TODO: Calculate how many are in each quadrant,
    multiply these four (4) sums by each other to calculate
    (Any robots along the center lines (horizontal/vertical) do not count
    the "Safety Factor"
    *)

    printfn "\nDay14 - DONE\n"

let test2 =
    let data = testData1

    let newLocationConfigured = newLocation 11 7

    let allTokens = getAllTokens data

    let robot = allTokens.Head

    let final = moveOneFor newLocationConfigured 4 robot

    printfn $"final: %A{final}"

    ()

let test1 testing =

    let data = testData1

    let newLocationConfigured = newLocation 11 7

    let allTokens = getAllTokens data

    let robot = allTokens.Head

    if testing then
        printfn $"%A{robot}"

    let newLocation1 = newLocationConfigured robot

    if testing then
        printfn $"newLocation1: %A{newLocation1}"

    let newLocation2 = newLocationConfigured newLocation1

    if testing then
        printfn $"newLocation2: %A{newLocation2}"

    let newLocation3 = newLocationConfigured newLocation2

    if testing then
        printfn $"newLocation3: %A{newLocation3}"

    let newLocation4 = newLocationConfigured newLocation3

    if testing then
        printfn $"newLocation4: %A{newLocation4}"

    ()
