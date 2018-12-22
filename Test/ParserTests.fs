module ParserTests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Logic
open Parsing

[<TestClass>]
type ParserTests () =
    static member variables = Map.ofList [ 'P', true ; 'Q', false ]

    static member Test (input: string, value: bool) =
        let expr = parse input

        match expr with
        | Some x -> Assert.AreEqual (x.Apply(ParserTests.variables), value)
        | None -> Assert.Fail()

    [<TestMethod>]
    member this.Expression_1 () =
        ParserTests.Test("(((!P) => Q) & (!P)) => Q", true)

    [<TestMethod>]
    member this.Not () =
        ParserTests.Test("!F", true)
        ParserTests.Test("!T", false)

    [<TestMethod>]
    member this.And () =
        ParserTests.Test("F & F", false)
        ParserTests.Test("F & T", false)
        ParserTests.Test("T & F", false)
        ParserTests.Test("T & T", true)

    [<TestMethod>]
    member this.Or () =
        ParserTests.Test("F | F", false)
        ParserTests.Test("F | T", true)
        ParserTests.Test("T | F", true)
        ParserTests.Test("T | T", true)

    [<TestMethod>]
    member this.Implies () =
        ParserTests.Test("F => F", true)
        ParserTests.Test("F => T", true)
        ParserTests.Test("T => F", false)
        ParserTests.Test("T => T", true)

        ParserTests.Test("F <= F", true)
        ParserTests.Test("F <= T", false)
        ParserTests.Test("T <= F", true)
        ParserTests.Test("T <= T", true)

    [<TestMethod>]
    member this.Equivalence () =
        ParserTests.Test("F <=> F", true)
        ParserTests.Test("F <=> T", false)
        ParserTests.Test("T <=> F", false)
        ParserTests.Test("T <=> T", true)

    [<TestMethod>]
    member this.Xor () =
        ParserTests.Test("F ^ F", false)
        ParserTests.Test("F ^ T", true)
        ParserTests.Test("T ^ F", true)
        ParserTests.Test("T ^ T", false)

