module Catamorphism

type Book = { title: string; price: decimal }

type ChocolateType =
    | Dark
    | Milk
    | SeventyPercent

type Chocolate =
    { chocType: ChocolateType
      price: decimal }

type WrappingPaperStyle =
    | HappyBirthday
    | HappyHolidays
    | SolidColor

type Gift =
    | Book of Book
    | Chocolate of Chocolate
    | Wrapped of Gift * WrappingPaperStyle
    | Boxed of Gift
    | WithACard of Gift * message: string

let wolfHall = { title = "Wolf Hall"; price = 20m }

let yummyChoc =
    { chocType = SeventyPercent
      price = 5m }

let birthdayPresent =
    WithACard (Wrapped (Book wolfHall, HappyBirthday), "Happy Birthday Frances!")

let christmasPresent = Wrapped (Boxed (Chocolate yummyChoc), HappyHolidays)

let rec description gift =
    match gift with
    | Book book -> sprintf "'%s'" book.title
    | Chocolate choc -> sprintf "%A chocolate" choc.chocType
    | Wrapped (innerGift, style) -> sprintf "%s wrapped in %A paper" (description innerGift) style
    | Boxed innerGift -> sprintf "%s in a box" (description innerGift)
    | WithACard (innerGift, message) -> sprintf "%s with a card saying '%s'" (description innerGift) message

let test1 = birthdayPresent |> description

let test2 = christmasPresent |> description

let rec totalCost gift =
    match gift with
    | Book book -> book.price
    | Chocolate choc -> choc.price
    | Wrapped (innerGift, _ (*style*) ) -> (totalCost innerGift) + 0.5m
    | Boxed innerGift -> (totalCost innerGift) + 1.0m
    | WithACard (innerGift, _ (*message*) ) -> (totalCost innerGift) + 2.0m

let test3 = birthdayPresent |> totalCost
let test4 = christmasPresent |> totalCost

let rec whatsInside gift =
    match gift with
    | Book _ (*book*) -> "A book"
    | Chocolate _ (*choc*) -> "Some chocolate"
    | Wrapped (innerGift, _ (*style*) ) -> whatsInside innerGift
    | Boxed innerGift -> whatsInside innerGift
    | WithACard (innerGift, _ (*message*) ) -> whatsInside innerGift

let test5 = birthdayPresent |> whatsInside
let test6 = christmasPresent |> whatsInside

let rec cataGift fBook fChocolate fWrapped fBox fCard gift : 'r =
    let recurse = cataGift fBook fChocolate fWrapped fBox fCard

    match gift with
    | Book book -> fBook book
    | Chocolate choc -> fChocolate choc
    | Wrapped (gift, style) -> fWrapped (recurse gift, style)
    | Boxed gift -> fBox (recurse gift)
    | WithACard (gift, message) -> fCard (recurse gift, message)

let rec totalCostUsingCata gift =
    let fBook (book: Book) = book.price
    let fChocolate (choc: Chocolate) = choc.price
    let fWrapped (innerCost, _ (*style*) ) = innerCost + 0.5m
    let fBox innerCost = innerCost + 1.0m
    let fCard (innerCost, _ (*message*) ) = innerCost + 2.0m
    // call the catamorphism
    cataGift fBook fChocolate fWrapped fBox fCard gift

let descriptionUsingCata gift =
    let fBook (book: Book) = sprintf "'%s'" book.title
    let fChocolate (choc: Chocolate) = sprintf "%A chocolate" choc.chocType

    let fWrapped (innerText, style) =
        sprintf "%s wrapped in %A paper" innerText style

    let fBox innerText = sprintf "%s in a box" innerText

    let fCard (innerText, message) =
        sprintf "%s with a card saying '%s'" innerText message
    // call the catamorphism
    cataGift fBook fChocolate fWrapped fBox fCard gift
(*
https://fsharpforfunandprofit.com/posts/recursive-types-and-folds/
https://en.wikipedia.org/wiki/Catamorphism
*)
let Run =
    printf "%s\n" test1
    printf "%s\n" test2
    printf "%f\n" test3
    printf "%f\n" test4
    printf "%s\n" test5
    printf "%s\n" test6
    printf "In the 'Catamorphism' example..."
