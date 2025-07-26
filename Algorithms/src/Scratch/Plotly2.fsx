#!/usr/bin/env -S dotnet fsi
#r "nuget:FSharp.Data"
#r "nuget: Plotly.NET, 3.0.1"

open FSharp.Data
open Plotly.NET

type LondonBoroughs = HtmlProvider<"https://en.wikipedia.org/wiki/List_of_London_boroughs">
let boroughs = LondonBoroughs.GetSample().Tables.``List of boroughs and local authorities``

let population =
    boroughs.Rows
    |> Array.map (fun row ->
                  row.Borough,
                  row.``Population (2022 est)``)
    |> Array.sortBy snd
    |> Chart.Column
    |> Chart.show