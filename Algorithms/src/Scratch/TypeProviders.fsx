#r "nuget: FSharp.Data"

open System
open FSharp.Data

// Define the type based on a sample JSON entry
type PeopleJson = JsonProvider<"""
[
  { "name": "Alice", "age": 30, "skills": ["F#", "C#", "Haskell"] }
]
""">

// Simulated JSON list (could be loaded from file or API)
let jsonListString = """
[
  { "name": "Alice",  "age": 30, "skills": ["F#", "C#", "Haskell"] },
  { "name": "Bob",    "age": 25, "skills": ["F#", "Rust"] },
  { "name": "Carol",  "age": 28, "skills": ["OCaml", "Elixir"] },
  { "name": "Dave",   "age": 35, "skills": ["Scala", "F#"] },
  { "name": "Eve",    "age": 32, "skills": ["Python", "F#", "ML"] },
  { "name": "Frank",  "age": 29, "skills": ["Clojure", "F#"] },
  { "name": "Grace",  "age": 27, "skills": ["TypeScript", "Elm"] },
  { "name": "Heidi",  "age": 33, "skills": ["Haskell", "PureScript"] },
  { "name": "Ivan",   "age": 31, "skills": ["Racket", "F#"] },
  { "name": "Judy",   "age": 26, "skills": ["ReasonML", "F#"] }
]
"""

// Parse the JSON
let people = PeopleJson.Parse(jsonListString)

// Print it
printfn "People in the list:\n"
for p in people do
    printfn $"%s{p.Name} (age %d{p.Age}) knows:"
    p.Skills |> Array.iter (printfn "  - %s")
    printfn ""
