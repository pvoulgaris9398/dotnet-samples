module TwoSums =

    let build (input: arr<int>) target =
        input
        |> Array.map(fun (x,y) -> (target - y, x))

    let run input =
        printf build [1,0,5,4,5]
