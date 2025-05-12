module Program

[<EntryPoint>]
let main args =

    do printfn "This is a 'do' binding example..."

    printfn "\n"
    printfn "********** STARTING TESTS **********"

    Day13.Test1

    printfn "********** ALL TESTS DONE **********"

    0
