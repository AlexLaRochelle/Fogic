module Logic

exception FromCharException of string

type UnaryOperator = 
    | Not
    override this.ToString() =
        match this with
        | Not -> "!"

    static member FromChar (str: char) =
        match str with
        | '!' -> Not
        | _ -> raise (FromCharException "Fuck")

    member this.apply = not
   

type BinaryOperator = 
    | And
    | Or
    | Xor
    | ImplicationLeftToRight
    | ImplicationRightToLeft
    | Equivalence

    override this.ToString() =
        match this with
        | And -> "&"
        | Or -> "|"
        | Xor -> "^"
        | ImplicationLeftToRight -> "=>"
        | ImplicationRightToLeft -> "<="
        | Equivalence -> "<=>"
    
    static member FromChar (str: char) =
        match str with
        | '&' -> And
        | '|' -> Or
        | '^' -> Xor
        | '>' -> ImplicationLeftToRight
        | '<' -> ImplicationRightToLeft
        | '=' -> Equivalence
        | _ -> raise (FromCharException "Fuck")
    
    member this.Apply = 
        let implies p q = (not p) || q
        match this with
        | And -> (&&)
        | Or -> (||) 
        | Xor -> fun p q -> (p || q) && (not(p && q))
        | ImplicationLeftToRight -> implies
        | ImplicationRightToLeft -> fun p q -> implies q p
        | Equivalence -> fun p q -> p = q


type Expression =
    | Value of bool
    | Variable of char
    | Unary of UnaryOperator * Expression
    | Binary of Expression * BinaryOperator * Expression

    override this.ToString() = 
        match this with
        | Value x -> x.ToString()
        | Variable x -> x.ToString()
        | Unary(op, x) -> op.ToString() + x.ToString()
        | Binary(x, op, y) -> "(" + String.concat " " [x.ToString() ; op.ToString() ; y.ToString()] + ")"

    member this.Apply(variables: Map<char, bool>) =
        match this with
        | Value x -> x
        | Variable x -> variables.[x]
        | Unary (o, e) -> o.apply(e.Apply(variables))
        | Binary (l, o, r) -> o.Apply (l.Apply(variables)) (r.Apply(variables))

