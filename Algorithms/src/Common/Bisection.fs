module Bisection

let epsilon = 1e-6

// Handle case where a and b do not bracket the root
let rec Solve (f: float -> float) (a: float) (b: float) =
    let fa = f a
    let fb = f b

    if abs (b - a) < epsilon then
        (a + b) / 2.0
    else
        let c = (a + b) / 2.0
        let fc = f c
        if (fa * fc < 0.0) then Solve f a c else Solve f c b
