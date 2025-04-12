module Cards

type Suit =
    | Hearts
    | Clubs
    | Diamonds
    | Spades

type Rank =
    | Value of int
    | Ace
    | King
    | Queen
    | Jack

    static member GetAllRanks() =
        [ yield Ace
          for i in 2..10 do
              yield Value i
          yield Jack
          yield Queen
          yield King ]

type Card = { Suit: Suit; Rank: Rank }

let fullDeck =
    [ for suit in [ Hearts; Diamonds; Clubs; Spades ] do
          for rank in Rank.GetAllRanks () do
              yield { Suit = suit; Rank = rank } ]

let showPlayingCard (c: Card) =
    let rankString =
        match c.Rank with
        | Ace -> "Ace"
        | King -> "King"
        | Queen -> "Queue"
        | Jack -> "Jack"
        | Value n -> string n

    let suitString =
        match c.Suit with
        | Hearts -> "Hearts"
        | Clubs -> "Clubs"
        | Diamonds -> "Diamonds"
        | Spades -> "Spades"

    rankString + " of " + suitString

let printAllCards () =
    for card in fullDeck do
        printfn $"{showPlayingCard card}"
