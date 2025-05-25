module Covariance

open Plotly.NET
open FSharp.Stats

let rnd = System.Random ()
let error () = rnd.Next (11)

let sampleA = Vector.init 50 (fun x -> float x)
let sampleB = Vector.init 50 (fun x -> float (x + error ()))
let sampleBHigh = sampleB |> Vector.map (fun x -> 200. + x)
let sampleC = Vector.init 50 (fun x -> 100. - float (x + 3 * error ()))
let sampleD = Vector.init 50 (fun x -> 100. + float (10 * error ()))

let covAB = Vector.cov sampleA sampleB
let covAC = Vector.cov sampleA sampleC
let covAD = Vector.cov sampleA sampleD
let covABHigh = Vector.cov sampleA sampleBHigh

let covPopAB = Vector.covPopulation sampleA sampleB
let covPopAC = Vector.covPopulation sampleA sampleC
let covPopAD = Vector.covPopulation sampleA sampleD
let covPopABHigh = Vector.covPopulation sampleA sampleBHigh

open Correlation
let pearsonAB = Seq.pearson sampleA sampleB
let pearsonAC = Seq.pearson sampleA sampleC
let pearsonAD = Seq.pearson sampleA sampleD
let pearsonABHigh = Seq.pearson sampleA sampleBHigh

let sampleChart =
    [ Chart.Point (sampleA, sampleB, "AB")
      Chart.Point (sampleA, sampleC, "AC")
      Chart.Point (sampleA, sampleD, "AD")
      Chart.Point (sampleA, sampleBHigh, "AB+") ]
    |> Chart.combine
    |> Chart.withTemplate ChartTemplates.lightMirrored
    |> Chart.withXAxisStyle "x"
    |> Chart.withYAxisStyle "y"
    |> Chart.withTitle "Test Cases for Covariance Calculation"
