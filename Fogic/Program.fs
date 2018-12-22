open System
open Logic
open Parsing

[<EntryPoint>]
let main argv =
    System.Console.InputEncoding <- System.Text.Encoding.UTF8
    System.Console.OutputEncoding <- System.Text.Encoding.UTF8

    let expr = parse "(((!P) => Q) & (!P)) => Q"
    let variables = Map.ofList [ 'P', true ; 'Q', false ]

    match expr with
    | Some x -> System.Console.WriteLine (sprintf "%s resolves to %s" (x.ToString()) (x.Apply(variables).ToString()))
    | None -> System.Console.WriteLine "Fuck this shit"

    System.Console.Read() |> ignore
    0