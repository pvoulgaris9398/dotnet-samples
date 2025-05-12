module Coordinates

// https://thinkbeforecoding.com/post/2020/10/07/applicative-computation-expressions

open FSharp.Data
open System

type ZipCode = JsonProvider<"http://api.zippopotam.us/GB/EC1">

let coord (zip: ZipCode.Root) =
    zip.Places.[0].Latitude, zip.Places.[0].Longitude

let dist (lata: decimal, longa: decimal) (latb: decimal, longb: decimal) =
    let x = float (longb - longa) * cos (double (latb + lata) / 2. * Math.PI / 360.)
    let y = float (latb - lata)
    let z = sqrt (x * x + y * y)
    z * 1.852 * 60. |> decimal

type AsyncBuilder with
    member _.BindReturn(x: 'a Async, f: 'a -> 'b) : 'b Async =
        // this is the same as:
        // async { return f v }
        async.Bind(x, fun v -> async.Return(f v))

    member _.MergeSources(x: 'a Async, y: 'b Async) : ('a * 'b) Async =
        // this is the same as:
        // async {
        //    let! xa = Async.StartChild x
        //    let! ya = Async.StartChild y
        //    let! xv = xa  // wait x value
        //    let! yv = ya  // wait y value
        //    return xv, yv // pair values
        // }
        async.Bind(
            Async.StartChild x,
            fun xa ->
                async.Bind(
                    Async.StartChild y,
                    fun ya ->
                        async.Bind(
                            xa,
                            fun xv ->
                                async.Bind(
                                    ya,
                                    fun yv -> async.Return(xv, yv)

                                )
                        )
                )
        )

let test =
    async {
        let! parisCoords =
            async {
                let! paris = ZipCode.AsyncLoad "http://api.zippopotam.us/fr/75020"
                return coord paris
            }

        and! londonCoords =
            async {
                let! london = ZipCode.AsyncLoad "http://api.zippopotam.us/GB/EC1"
                return coord london
            }

        return dist parisCoords londonCoords
    }
    |> Async.RunSynchronously

let Run = test
