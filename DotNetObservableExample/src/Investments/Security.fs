namespace Securities

open System.Runtime.CompilerServices

type Cusip = private Cusip of string
type Isin = private Isin of string
type Sedol = private Sedol of string
type Ticker = private Ticker of string

module Cusip =
    let IsValid(str: string) =
        str.Length = 9

type Identifier = 
|IdCusip of Cusip
|IdIsin of Isin
|IdSedol of Sedol
|IdTicker of Ticker

type Security = private Identifier of Identifier

module Factory =

    let CreateValidSecurity(str:string) =
        match Cusip.IsValid str with
        | true -> Some(Cusip str)
        | _ -> None

    let FromCusip(str:string) =
        Some(Cusip str)

[<Extension>]
type StringExtensions =
    [<Extension>]
    static member ToCusip(str: string) =
        match Cusip.IsValid str with
        | true -> Some(Cusip str)
        | _ -> None
