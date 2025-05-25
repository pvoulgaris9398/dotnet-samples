module Day15

[<Literal>]
let testData1 = "<^^>>>vv<v>>v<<"

let testData2: seq<string> =
    seq {
        yield "########"
        yield "#..O.O.#"
        yield "##@.O..#"
        yield "#...O..#"
        yield "#.#.O..#"
        yield "#...O..#"
        yield "#......#"
        yield "########"
    }

type Point =
    | Box of int * int
    | Wall of int * int
    | Robot of int * int
    | Empty of int * int

    override this.ToString() =
        match this with
        | Box(x, y) -> "Box"
        | Wall(x, y) -> "Wall"
        | Robot(x, y) -> "Robot"
        | Empty(x, y) -> "Empty"

type Move =
    | Up of char
    | Down of char
    | Left of char
    | Right of char

    override this.ToString() =
        match this with
        | Up(c) -> $"Up ({c})"
        | Down(c) -> $"Down ({c})"
        | Left(c) -> $"Left ({c})"
        | Right(c) -> $"Right ({c})"

let getMoves testData =
    testData
    |> Seq.map (fun c ->
        match c with
        | 'v' -> Down c
        | '^' -> Up c
        | '<' -> Left c
        | '>' -> Right c
        | _ -> failwith "Invalid character!")

let getPoints testData =
    testData
    |> Seq.mapi (fun i _ ->
        Seq.mapi (fun j c ->
            let result =
                match c with
                | '#' -> Wall(i, j)
                | '@' -> Robot(i, j)
                | '.' -> Empty(i, j)
                | 'O' -> Box(i, j)
                | _ -> failwith "Invalid character!"

            result)
        |> Seq.collect)

///
/// Day 14 of: https://adventofcode.com/2024
///
let Run (testing: bool) =

    printfn "\nDay15 of advent of code 2024\n"

    let moves = getMoves testData1 |> Seq.iter (fun m -> printfn $"%O{m}")

    let points = getPoints testData2 |> Seq.iter (fun m -> printfn $"%A{m}")

    printfn "\nDay14 - DONE\n"

    ()
