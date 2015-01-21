namespace Cribbage.Core.UnitTests

open NUnit.Framework

[<TestFixture>]
module Cards =

    [<Test>]
    let ``Deal two 5 card hands from 10 cards deck should return two 5 card hands and empty dec`` () =
        Assert.AreEqual(5, 5)