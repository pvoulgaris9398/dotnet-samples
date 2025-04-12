module BST

open System

type BST<'T> =
    | Empty
    | Node of value: 'T * left: BST<'T> * right: BST<'T>

let rec exists item bst =
    match bst with
    | Empty -> false
    | Node (x, left, right) ->
        if item = x then true
        elif item < x then (exists item left)
        else (exists item right)

let rec insert item bst =
    match bst with
    | Empty -> Node (item, Empty, Empty)
    | Node (x, left, right) as node ->
        if item = x then node
        elif item < x then Node (x, insert item left, right)
        else Node (x, left, insert item right)

type Person = { First: string; Last: string }

type Employee =
    | Engineer of engineer: Person
    | Manager of manager: Person * reports: List<Employee>
    | Executive of executive: Person * reports: List<Employee> * assistant: Employee

let rec countReports (emp: Employee) =
    1
    + match emp with
      | Engineer (person) -> 0
      | Manager (person, reports) -> reports |> List.sumBy countReports
      | Executive (person, reports, assistant) -> (reports |> List.sumBy countReports) + countReports assistant

let findDaveWithOpenPositions (emps: List<Employee>) =
    emps
    |> List.filter (function
        | Manager ({ First = "Dave" }, []) -> true
        | Executive ({ First = "Dave" }, [], _) -> true
        | _ -> false)

let private parseHelper (f: string -> bool * 'T) =
    f
    >> function
        | (true, item) -> Some item
        | (false, _) -> None

let parseDateTimeOffset = parseHelper DateTimeOffset.TryParse

let result = parseDateTimeOffset "1970-01-01"

match result with
| Some dto -> printfn "It parsed!"
| None -> printfn "It didn't parse!"

let parseInt = parseHelper Int32.TryParse
let parseDouble = parseHelper Double.TryParse
let parseTimeSpan = parseHelper TimeSpan.TryParse

let (|Int|_|) = parseInt
let (|Double|_|) = parseDouble
let (|Date|_|) = parseDateTimeOffset
let (|TimeSpan|_|) = parseTimeSpan

let printParseResult =
    function
    | Int x -> printfn $"%d{x}"
    | Double x -> printfn $"%f{x}"
    | Date d -> printf $"%O{d}"
    | TimeSpan t -> printf $"%O{t}"
    | _ -> printfn $"Nothing was parse-able!"

printParseResult "12"
printParseResult "3.14"
printParseResult "12/29/2022"
printParseResult "4:00 AM"
printParseResult "Banana!"
