namespace Common

open System.Runtime.CompilerServices
open System

[<Extension>]
type ListExtensions =
    [<Extension>]
    // This returns a System.Tuple, C# uses System.ValueTuple
    // I'd like to figure out how to return a System.ValueTuple here
    static member ToPairs(list: seq<'a>) : seq<Tuple<'a, 'a>> =
        list |> Seq.pairwise