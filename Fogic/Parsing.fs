module Parsing

open Parsec
open Logic

let unaryOperators = char '!'
let leftImplies = char '=' <*>> char '>'
let rightImples = char '<' <<*> char '='
let equivalence = (char '<' <*>> char '=' <<*> char '>')
let binaryOperators = char '&' <|> char '|' <|> char '^' <|> equivalence <|> leftImplies <|> rightImples

let exprSetter, expr = slot ()
let variable = letter |> map (fun x -> Variable x)
let constants = char 'F' <|> char 'T' |> map (fun x -> Value (x = 'T'))
let brack = char '(' <*>> anySpace <*>> expr <<*> anySpace <<*> char ')'
let term = constants <|> variable <|> brack
let binary = term <<*> anySpace <*> binaryOperators <<*> anySpace <*> term 
             |> map (fun ((l, op), r) -> Binary(l, BinaryOperator.FromChar op, r))

let unary = unaryOperators <*> term 
            |> map (fun (op, r) -> Unary(UnaryOperator.FromChar op, r))

let exprAux = unary <|> binary <|> term

exprSetter.Set exprAux

let parse input =
    try
        run expr input
    with
    | FromCharException x -> None


