module ComputationExpression1

type LoggingBuilder() =
    let log p = printfn "expression is %A" p

    member __.Bind(x, f) =
        log x
        f x

    member __.Return(x) = x

let Run =
    let logger = new LoggingBuilder()

    let loggedWorkflow =
        logger {
            let! x = 42
            let! y = 43
            let! z = x + y
            return z
        }

    let _ = loggedWorkflow

    ()
