module test2
    
    let sieve =
        let rec sieveH current primes =
            //printf "%i\n" current
            seq{
                yield current
                let next = primes |> Seq.find(fun x -> x > current)
                yield! sieveH next (primes |> Seq.filter(fun x -> x % current <> 0 ))
            }
        sieveH 2 (seq {2 .. 1000})

    (*
    Run the example
    *)
    let run = 
        let result = sieve |> Seq.takeWhile (fun x -> x < 100) |> List.ofSeq
        result |> Seq.iter(fun x -> printf "%i\n" x)

        let nthPrime n =
            sieve |> Seq.skip(n-1) |>Seq.head

        printf "The 100th prime number is: %i" (nthPrime 100)

        ()

