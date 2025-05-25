module UnitsOfMeasure

open Microsoft.FSharp.Data.UnitSystems.SI.UnitNames

let sampleValue1 = 1600.0<meter>

[<Measure>]
type mile =
    static member asMeter = 1609.34<meter / mile>

let sampleValue2 = 500.0<mile>

let sampleValue3 = sampleValue2 * mile.asMeter

type Customer =
    | Registered of Name: string * Email: string option * IsEligible: bool
    | Guest of Name: string

let calculatedOrderTotal customer spend =
    let discount =
        match customer with
        | Registered (IsEligible = true) when spend >= 100M -> spend * 0.1M
        | _ -> 0M

    spend - discount

let tryGetEligibleEmail customer =
    match customer with
    | Registered (IsEligible = true; Email = email) -> Some email
    | _ -> None
