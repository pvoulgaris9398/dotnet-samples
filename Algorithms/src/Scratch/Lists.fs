module Lists

open System

let valuesList = [ ("a", 1); ("b", 2); ("c", 3) ]

let resultPickTest =
    let resultPick =
        List.pick
            (fun elem ->
                match elem with
                | (value, 2) -> Some value
                | _ -> None)
            valuesList

    printfn "%A" resultPick
    ()

let tester =
    let list1 = [ 1; 2; 3; 4; 5; 6 ]
    let list2 = [ 2; 2; 2; 4; 5; 5 ]

    List.filter (fun x -> List.contains x list1) list2
    |> List.iter (fun x -> printfn "%i" x)

    printfn "Tester"

    ()

let zipTest =
    let list1 = [ 1; 2; 3 ]
    let list2 = [ -1; -2; -2 ]
    let list3 = [ 0; 0; 0 ]
    let listZipped = List.zip3 list1 list2 list3
    printfn "%A" listZipped
    ()

let unzipTest =
    let lists = List.unzip [ (1, 2); (3, 4) ]
    printfn "%A" lists
    printfn "%A %A" (fst lists) (snd lists)
    ()

let collectListTest =
    let list1 = [ 1; 2; 3 ]
    let collectList = List.collect (fun x -> [ for i in 1..3 -> x * i ]) list1
    printfn "%A" collectList
    ()

let listWordsTest =
    let listWords = [ "and"; "Rome"; "Bob"; "apple"; "zebra" ]
    let isCapitalized (string1: string) = System.Char.IsUpper string1[0]

    let results =
        List.choose
            (fun elem ->
                match elem with
                | elem when isCapitalized elem -> Some (elem + "'s")
                | _ -> None)
            listWords

    printfn "%A" results

let (|Split|) (on: char) (s: string) =
    s.Split (on, StringSplitOptions.RemoveEmptyEntries ||| StringSplitOptions.TrimEntries)
    |> Array.toList
