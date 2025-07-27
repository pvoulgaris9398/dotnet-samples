open System
open Common

[<EntryPoint>]
let main args =
    printfn "Reading command-line args!"

    let value, json = getJson{|args=args;year=System.DateTime.Now.Year|}
    printfn $"Input: %0A{value}"
    printfn $"Output: %s{json}"

    0