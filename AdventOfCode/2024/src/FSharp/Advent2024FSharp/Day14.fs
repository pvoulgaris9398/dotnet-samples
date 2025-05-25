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

let (width, height) = (101, 103)

type Robot =
    { Current: int * int
      Velocity: int * int }

let parseData pattern str =
    match str with
    | ParseRegex pattern [ ToInt32 x; ToInt32 y; ToInt32 dx; ToInt32 dy ] ->
        { Current = (x, y)
          Velocity = (dx, dy) }

    | _ -> failwith "Unable to parse!"

let newLocation xmax ymax robot : Robot =
    let (x, y) = robot.Current
    let (dx, dy) = robot.Velocity
    let deltax = x + dx
    let deltay = y + dy

    let newx =
        match deltax with
        | x when deltax > xmax -> deltax - xmax
        | x when deltax < 0 -> deltax + xmax
        | x when deltax >= 0 -> deltax
        | _ -> deltax

    let newy =
        match deltay with
        | y when deltay > ymax -> deltay - ymax
        | y when deltay < 0 -> deltay + ymax
        | y when deltay >= 0 -> deltay
        | _ -> deltay

    { robot with Current = (newx, newy) }

let getRobotPositionIn time width height robot =
    let cx, cy = robot.Current
    let vx, vy = robot.Velocity

    let newPos =
        ((cx + time * (vx + width)) % width, (cy + time * (vy + height)) % height)

    { robot with Current = newPos }

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

let quadrant (robot: Robot) =
    let halfx = width / 2
    let halfy = height / 2

    let result =
        match robot.Current with
        | (x, y) when x > 0 && x < halfx && y > 0 && y < halfy -> 1
        | (x, y) when x > halfx && x <= width && y > 0 && y < halfy -> 2
        | (x, y) when x > 0 && x < halfx && y > halfy && y <= height -> 3
        | (x, y) when x > halfx && x <= width && y > halfy && y <= height -> 4
        | _ -> 0

    result

let quadrant2 (robot: Robot) =
    let xHalf = width / 2
    let yHalf = height / 2
    let (x, y) = robot.Current
    x < xHalf, y < yHalf

let safetyFactor (quadrant: Robot -> int) (robots: seq<Robot>) =
    robots
    |> Seq.groupBy (fun x -> quadrant x)
    |> Seq.map (fun (key, items) -> key, Seq.length items)
    |> Seq.filter (fun (key, _) -> key > 0)
    |> Seq.map snd
    |> Seq.toList
    |> Seq.fold (fun x state -> x * state) 1

let safetyFactor2 (robots: seq<Robot>) =
    let xHalf = width / 2
    let yHalf = height / 2

    let getQuadrant robot =
        let x, y = robot.Current
        x < xHalf, y < yHalf

    Seq.filter
        (fun robot ->
            let (x, y) = robot.Current
            x <> xHalf && y <> yHalf)
        robots
    |> Seq.groupBy getQuadrant
    |> Seq.map (fun ((l, r), robots) ->
        printfn "%b %b %A" l r (Seq.length robots)
        Seq.length robots)
    |> Seq.reduce (fun x y -> x * y)

///
/// Day 14 of: https://adventofcode.com/2024
///
let Run (testing: bool) =

    printfn "\nDay14 of advent of code 2024\n"

    let data = if testing then testData2 else allText fileName

    let allRobots = getAllTokens data

    if testing then
        allRobots |> List.iter (fun x -> printfn $"%A{x}")

    let movements = List.map (getRobotPositionIn 100 width height) allRobots

    if testing then
        movements |> Seq.iter (fun x -> printfn $"%A{x}")

    let sf1 = safetyFactor2 movements

    printfn $"Safety Factor 1: %A{sf1}"

    printfn "\nDay14 - DONE\n"

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
