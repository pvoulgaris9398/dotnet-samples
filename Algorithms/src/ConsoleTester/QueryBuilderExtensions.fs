module QueryBuilderExtensions

open System
open FSharp.Linq

type QueryBuilder with

    [<CustomOperation>]
    member _.any(source: QuerySource<'T, 'Q>, predicate) =
        System.Linq.Enumerable.Any (source.Source, Func<_, _> (predicate))

    [<CustomOperation("singleSafe")>] // you can specify your own operation name in the constructor
    member _.singleOrDefault(source: QuerySource<'T, 'Q>, predicate) =
        System.Linq.Enumerable.SingleOrDefault (source.Source, Func<_, _> (predicate))
