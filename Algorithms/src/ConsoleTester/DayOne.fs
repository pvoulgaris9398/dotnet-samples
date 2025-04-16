module DayOne

[<Literal>]
let fileName = "..\\..\\..\\..\\..\\day1.txt"

let splitIntoTuple (str: string) (separator: string) : string * string =
    let parts = str.Split (separator)
    (parts[0], parts[1])

let Run =
    let data = FileReader.lines fileName

    data
    |> Seq.map (fun line -> splitIntoTuple line " ")
    |> Seq.iter (fun tuple -> printfn "%A" tuple)

//
