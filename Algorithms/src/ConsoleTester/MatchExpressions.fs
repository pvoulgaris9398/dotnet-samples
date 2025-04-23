module MatchExpressions

(*

let rec loopAndPrint aList =
    match aList with
    // Empty...Done
    | [] -> printfn "empty"
    // binding to head::tail
    | x :: xs ->
        printfn "element=%A," x
        loopAndPrint xs

let rec loopAndSum aList sumSoFar =
    match aList with
    | [] -> sumSoFar
    | x :: xs ->
        let newSumSoFar = sumSoFar + x
        loopAndSum xs newSumSoFar

loopAndPrint [ 1..5 ]
let result = loopAndSum [ 1..5 ] 0
printfn "%i" result

let aTuple = (1, 2)

match aTuple with
| (1, _) -> printfn "first part was 1"
| (_, 2) -> printfn "second was 2"
| _ -> failwith "We should not have gotten here!"

let fizzbuzz x =
    match x with
    | i when i % 15 = 0 -> printfn "fizzbuzz"
    | i when i % 3 = 0 -> printfn "fizz"
    | i when i % 5 = 0 -> printfn "buzz"
    | i -> printfn "%i" i

[ 1..30 ] |> List.iter fizzbuzz

open System.Text.RegularExpressions

let (|EMailAddress|_|) input =
    let m = Regex.Match (input, @".+@.+")
    if (m.Success) then Some input else None

let classifyString aString =
    match aString with
    | EMailAddress x -> printfn "%s is an e-mail" x
    | _ -> printfn "%s is something else" aString

classifyString "dude@bancroft.com"
classifyString "artemis.gov"

let times6 x = x * 6

let isAnswerToEverything x =
    match x with
    | 42 -> (x, true)
    | _ -> (x, false)

let result = [ 1..10 ] |> List.map (times6 >> isAnswerToEverything)
result |> List.iter (fun x -> printfn "%A" x)

*)

let xs =
    seq {
        for i in 1..10 do
            yield i * i
    }

xs |> Seq.iter (fun x -> printfn "%i" x)

let squares = seq { for i in 1..10 -> i * i }

let cubes = seq { for i in 1..10 -> i * i * i }

let squaresAndCubes =
    seq {
        yield! squares
        yield! cubes
    }

let Run = printfn "Done"
