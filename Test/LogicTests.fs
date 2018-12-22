namespace Test


open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Logic


[<TestClass>]
type LogicTests () =

    [<TestMethod>]
    member this.Expression_1 () =
        let x = Variable 'X'
        let y = Variable 'Y'
        let expr = Binary(x, Equivalence, y)
        let variables = Map.ofList [ 'X', true ; 'Y', true ]

        Assert.IsTrue(expr.Apply(variables));

    [<TestMethod>]
    member this.Expression_2 () =
        let x = Variable 'X'
        let y = Variable 'Y'
        let expr = Binary(x, Xor, y)
        let variables = Map.ofList [ 'X', true ; 'Y', true ]

        Assert.IsFalse(expr.Apply(variables));

    [<TestMethod>]
    member this.Expression_3 () =
        let x = Variable 'X'
        let y = Variable 'Y'
        let z = Variable 'Z'
        let expr = Binary(x, ImplicationLeftToRight, Binary(y, And, z))
        let variables = Map.ofList [ 'X', true ; 'Y', true ; 'Z', false]

        Assert.IsFalse(expr.Apply(variables));

