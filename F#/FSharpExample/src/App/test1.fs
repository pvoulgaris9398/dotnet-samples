module test1

    let printSeq seq1 = Seq.iter(printf "%A " ) seq1; printfn ""

    let run =
    
        let seqNumbers = [1.0;1.5;2.0;1.5;1.0;1.5 ] |> seq<float>
        let seqWindows = Seq.windowed 3 seqNumbers
        let seqMovingAverage = Seq.map Array.average seqWindows

        printfn "Initial Sequence: "
        printSeq seqNumbers
        printfn "\nWindows of length: 3 "
        printSeq seqWindows
        printfn "\nMoving average: "
        printSeq seqMovingAverage

        ()
