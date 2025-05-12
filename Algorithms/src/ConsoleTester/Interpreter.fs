module Interpreter

open System

type Program<'a> =
    | ReadLn of unit * next: (string -> Program<'a>)
    | WriteLn of string * next: (unit -> Program<'a>)
    | Stop of 'a

let readFromConsole =
    WriteLn(
        "Enter the first value",
        fun () ->
            ReadLn(
                (),
                fun str1 -> WriteLn("Enter the second value", fun () -> ReadLn((), fun str2 -> Stop(str1, str2)))
            )
    )

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
    let _ = interpret readFromConsole
    ()
