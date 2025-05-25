module FileReader

open System.IO

let lines fileName = File.ReadLines (fileName)

let allText fileName = File.ReadAllText(fileName)

let Run fileName =
    lines fileName |> Seq.iter (fun line -> printfn "%s" line)
