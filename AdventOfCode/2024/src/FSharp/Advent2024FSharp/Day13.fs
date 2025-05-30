﻿module Day13

open System.IO

type Machine =
    { ax: int64
      bx: int64
      ay: int64
      by: int64
      xGoal: int64
      yGoal: int64 }

let trim (input: string) = input.Trim()

let split (splitter: string) (input: string) = input.Split(splitter)

let substring (startIndex: int) (input: string) = input.Substring(startIndex)

let parseLine s =
    let parts =
        s
        |> split ": "
        |> Array.item 1
        |> split ", "
        |> Array.map (substring 2 >> int64)

    match parts with
    | [| x; y |] -> (x, y)
    | _ -> failwith "button ?"

let parseMachine s =
    match s |> split "\n" with
    | [| xLine; yLine; goalLine |] ->
        let (ax, ay) = parseLine xLine
        let (bx, by) = parseLine yLine
        let (gx, gy) = parseLine goalLine

        { ax = ax
          bx = bx
          ay = ay
          by = by
          xGoal = gx
          yGoal = gy }
    | _ -> failwith "machine ?"

let calculateA m =
    (m.yGoal * m.bx - m.xGoal * m.by) / (m.bx * m.ay - m.by * m.ax)

let calculateB a m = (m.xGoal - m.ax * a) / m.bx

let calculate clickConstraint m =
    let a = m |> calculateA
    let b = m |> calculateB a
    let x = a * m.ax + b * m.bx
    let y = a * m.ay + b * m.by

    if clickConstraint a && clickConstraint b && x = m.xGoal && y = m.yGoal then
        Some(3L * a + b)
    else
        None

let calculate1 = calculate (fun clicks -> clicks <= 100)

let adjustGoals m =
    { m with
        xGoal = m.xGoal + 10000000000000L
        yGoal = m.yGoal + 10000000000000L }

let calculate2 = adjustGoals >> calculate (fun _ -> true)

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day13.txt"

[<Literal>]
let testData1 =
    "Button A: X+94, Y+34\r\nButton B: X+22, Y+67\r\nPrize: X=8400, Y=5400"

///
/// Day 13 of: https://adventofcode.com/2024
///
let Run =

    printfn "\nDay13 of advent of code 2024\n"

    let chunks = File.ReadAllText fileName |> trim |> split "\r\n\r\n" |> Array.toList
    let machines = chunks |> List.map parseMachine
    machines |> List.choose calculate1 |> List.sum |> printfn "%d"
    machines |> List.choose calculate2 |> List.sum |> printfn "%d"

    printfn "\nDay13 - DONE\n"

let Test1 =
    let chunks = testData1 |> trim |> split "\r\n\r\n" |> Array.toList
    let machines = chunks |> List.map parseMachine
    machines |> List.choose calculate1 |> List.sum |> printfn "%d"
    machines |> List.choose calculate2 |> List.sum |> printfn "%d"
