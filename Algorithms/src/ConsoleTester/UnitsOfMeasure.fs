module UnitsOfMeasure

open Microsoft.FSharp.Data.UnitSystems.SI.UnitNames

let sampleValue1 = 1600.0<meter>

[<Measure>]
type mile =
    static member asMeter = 1609.34<meter / mile>

let sampleValue2 = 500.0<mile>

let sampleValue3 = sampleValue2 * mile.asMeter
