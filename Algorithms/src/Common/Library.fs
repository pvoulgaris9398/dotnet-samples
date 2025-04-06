namespace Common

open System.Runtime.CompilerServices
open System

[<Extension>]
type ListExtensions =
    [<Extension>]
    // This returns a ValueTuple, which is what C# expects
    // TODO: Figure out how/best to do a null check here
    // static member ToPairs(list: seq<'a>) : seq<ValueTuple<'a, 'a>> =
    static member ToPairs list =
        list 
            |> Seq.pairwise
            |> Seq.map (fun (previous, current) -> struct(previous, current))