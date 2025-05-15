module Day04

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day4.txt"

let testData1: seq<string> =
    seq {
        yield "ABCD"
        yield "ABCD"
        yield "ABCD"
        yield "ABCD"
    }

let testData2: seq<string> =
    seq {
        yield "ABCDEFGH"
        yield "ABCDEFGH"
        yield "ABCDEFGH"
        yield "ABCDEFGH"
        yield "ABCDEFGH"
        yield "ABCDEFGH"
        yield "ABCDEFGH"
        yield "ABCDEFGH"
    }

let getInputData = FileReader.lines fileName

let getRowCount lines = lines |> Seq.length

let getColumnCount (lines: seq<string>) =
    match Seq.toList lines with
    | [] -> 0
    (*
    - Struggled with this one for a while, it turned-out to be an easy fix
    - Good posts on this:
    - https://www.compositional-it.com/news-blog/5-things-to-be-aware-of-with-f-anonymous-records/
    - https://fsharpforfunandprofit.com/posts/type-inference/
    FS0072
    Lookup on object of indeterminate type based on information prior to this program point.
    A type annotation may be needed prior to this program point to constrain the type of the object.
    This may allow the lookup to be resolved.
    *)
    | x -> x[0].Length

let toRows lines = lines

let toColumn (lines: seq<string>) i =
    lines |> Seq.map (fun row -> row[i]) |> Seq.toArray |> (fun x -> new string (x))

let toColumns (lines: seq<string>) =
    let columnCount = getColumnCount lines
    [ 0..columnCount-1 ] |> Seq.map (toColumn lines)

let toDiagonal (lines: seq<string>) startRow startColumn columnCount =
    lines |> Seq.skip startRow |> Seq.take (columnCount-startColumn) 
    |> Seq.mapi (fun i row -> row[startColumn+i])
    |> Seq.toArray|> (fun x -> new string (x))

let toDiagonals (lines: seq<string>) =
    let rowCount = getRowCount lines
    let columnCount = getColumnCount lines
    let temp1 = [ 0..columnCount-1 ]  |> Seq.map (fun columnIndex -> toDiagonal lines 0 columnIndex columnCount)
    //let temp2 = [ 1..rowCount-1 ]  |> Seq.map (fun rowIndex -> toDiagonal lines rowIndex 0 columnCount)
    temp1
    //Seq.append temp1 temp2

let toHorizontal verticals = ()

let getAllStrings verticals = ()

///
/// Day 4 of: https://adventofcode.com/2024
///
let Run (testing: bool) =
    printfn "\nDay04 of advent of code 2024\n"

    let data =
        match testing with
        | true -> testData1
        | false -> getInputData

    let rows = data |> toRows |> Seq.toList

    printfn $"Rows\n%A{rows}"

    let columns = data |> toColumns |> Seq.toList

    printfn $"Columns\n%A{columns}"

    let diagonals = data |> toDiagonals |> Seq.toList

    printfn $"Diagonals\n%A{diagonals}"

    printfn "\nDay04 - DONE\n"

module internal Tests =

    let test1 testing =
        let toColumn1 (lines: seq<string>) i = lines |> Seq.map (fun row -> row[i])

        let toColumns1 (lines: seq<string>) =
            let columnCount =
                match Seq.toList lines with
                | [] -> 0
                | x -> x[0].Length - 1

            [ 0..columnCount ] |> Seq.map (toColumn1 lines)

        let result = testData1 |> toColumns1

        if testing then
            printfn $"toColumns1\t%A{result}"

        let temp =
            result
            |> Seq.map (fun x -> x |> Seq.toArray)
            |> Seq.map (fun x -> new string (x))
            |> Seq.toList

        if testing then
            printfn $"temp\t%A{temp}"

    let test2 testing =
        let toColumn2 (lines: seq<string>) i =
            lines |> Seq.map (fun row -> row[i]) |> Seq.toArray |> (fun x -> new string (x))

        let toColumns2 (lines: seq<string>) =
            let columnCount =
                match Seq.toList lines with
                | [] -> 0
                | x -> x[0].Length - 1

            [ 0..columnCount ] |> Seq.map (toColumn2 lines)

        let result = testData1 |> toColumns2

        if testing then
            printfn $"toColumns2\t%A{result}"
