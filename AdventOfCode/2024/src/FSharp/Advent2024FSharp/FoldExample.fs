module FoldExample

let (count, sum) = (0, 0)

let initial = (0, 0)

let update (count, sum) value =
    if value > 0 then (count + 1, sum)
    elif value = 0 then (count, sum)
    else (count, sum + value)

let runTest inputList = inputList |> List.fold update initial

let Run =
    let input = [ 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; -11; -12; -13; -14; -15 ]
    let (count, sum) = runTest input
    printfn "Count: %i, Sum: %i" count sum
