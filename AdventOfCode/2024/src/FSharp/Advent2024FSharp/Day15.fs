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

let listTo2DArray (stringList: string seq) (columns: int) : char[,] =
    let rows = Seq.length stringList
    let array2D = Array2D.create rows columns '\000'

    stringList
    |> Seq.iteri (fun index value ->
        let c = value.[0]
        let row = index / columns
        let col = index % columns
        array2D.[row, col] <- c)

    array2D

let filterArray map pred =
    seq {
        for x in 0 .. (Array2D.length1 map - 1) do
            for y in 0 .. (Array2D.length1 map - 1) -> if pred map[x, y] then Some(x, y) else None

    }
    |> Seq.choose id

let getPoints data =
    seq {
        for y in 0 .. (Array2D.length2 data - 1) do
            for x in 0 .. (Array2D.length1 data - 1) do
                let result =
                    match data.[x, y] with
                    | '#' -> Wall(x, y)
                    | '@' -> Robot(x, y)
                    | '.' -> Empty(x, y)
                    | 'O' -> Box(x, y)
                    | _ -> failwith "Invalid character!"

                yield result
    }

let getPoints2 (data: seq<string>) : (seq<Point>) =
    seq {
        let columnCount = Seq.item 0 data |> String.length

        for rowIndex in 0 .. Seq.length data - 1 do
            for columnIndex in 0 .. columnCount - 1 do
                let row = Seq.item rowIndex data
                let cell = Seq.item columnIndex row

                let result =
                    match cell with
                    | '#' -> Wall(rowIndex, columnIndex)
                    | '@' -> Robot(rowIndex, columnIndex)
                    | '.' -> Empty(rowIndex, columnIndex)
                    | 'O' -> Box(rowIndex, columnIndex)
                    | _ -> failwith "Invalid character!"

                yield result
    }

///
/// Day 14 of: https://adventofcode.com/2024
///
let Run (testing: bool) =

    printfn "\nDay15 of advent of code 2024\n"

    let moves = getMoves testData1 |> Seq.iter (fun m -> printfn $"%O{m}")

    //let columns = testData2 |> Seq.item 0 |> String.length
    //let data2 = listTo2DArray testData2 columns

    let points =
        getPoints2 testData2 |> Seq.toList |> Seq.iter (fun m -> printfn $"%A{m}")

    printfn "\nDay14 - DONE\n"

    ()
