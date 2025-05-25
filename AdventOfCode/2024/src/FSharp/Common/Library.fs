namespace Common

open System.Runtime.CompilerServices
open System.Collections.Generic
open System

type Pair(left: Int64, right: Int64) =
    member this.Left = left
    member this.Right = right

[<Extension>]
type ListExtensions =
    [<Extension>]
    // This returns a ValueTuple, which is what C# expects
    // TODO: Figure out how/best to do a null check here
    // static member ToPairs(list: seq<'a>) : seq<ValueTuple<'a, 'a>> =
    static member ToPairs list =
        list
        |> Seq.pairwise
        |> Seq.map (fun (previous, current) -> struct (previous, current))

    [<Extension>]
    static member ToPairs2(list: IEnumerable<Int64>) =
        list |> Seq.pairwise |> Seq.map (fun (left, right) -> Pair (left, right))
