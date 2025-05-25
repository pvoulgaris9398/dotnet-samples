type RegisteredCustomer = { Id: string; IsEligible: bool }

type UnregisteredCustomer = { Id: string }

type Customer =
    | Registered of RegisteredCustomer
    | Guest of UnregisteredCustomer


let calculateTotal customer spend =
    let discount =
        match customer with
        | Registered c ->
            if c.IsEligible && spend >= 100.0M then
                spend * 0.1M
            else
                0.0M
        | Guest _ -> 0.0M

    spend - discount

0
