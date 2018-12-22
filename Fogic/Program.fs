open System
open Logic
open Parsing


[<EntryPoint>]
let main argv =
    System.Console.InputEncoding <- System.Text.Encoding.UTF8
    System.Console.OutputEncoding <- System.Text.Encoding.UTF8

    // It's a tautology, so it is always true
    let expr = parse "(((!P) => Q) & (!P)) => Q"
    let variables = Map.ofList [ 'P', true ; 'Q', false ]

    match expr with
    | Some x -> System.Console.WriteLine (sprintf "%s resolves to %s" (x.ToString()) (x.Apply(variables).ToString()))
    | None -> System.Console.WriteLine "Something went wrong"

    System.Console.Read() |> ignore
    0