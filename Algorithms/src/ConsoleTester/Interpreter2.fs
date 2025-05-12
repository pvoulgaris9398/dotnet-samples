module Interpreter2

open System

type Program<'a> =
    | ReadLn of unit * next: (string -> Program<'a>)
    | WriteLn of string * next: (unit -> Program<'a>)
    | Stop of 'a

module Program =

    let rec bind f program =
        match program with
        | ReadLn((), next) -> ReadLn((), next >> bind f)
        | WriteLn(str, next) -> WriteLn(str, next >> bind f)
        | Stop x -> f x

type ProgramBuilder() =
    member _.Return(x) = Stop x
    member _.Bind(x, f) = Program.bind f x
    member _.Zero() = Stop()

let program = ProgramBuilder()

let writeLn str = WriteLn(str, Stop)
let readLn () = ReadLn((), Stop)

let readFromConsole2 =
    program {
        do! writeLn "Enter the first value please"
        let! str1 = readLn ()
        do! writeLn "Enter the second value please"
        let! str2 = readLn ()
        return (str1, str2)
    }

let rec interpret program =
    match program with
    | ReadLn((), next) ->
        let str = Console.ReadLine()
        let nextProgram = next str
        interpret nextProgram
    | WriteLn(str, next) ->
        printfn "%s" str
        let nextProgram = next ()
        interpret nextProgram
    | Stop value -> value

let Run =
    let _ = interpret readFromConsole2
    ()
