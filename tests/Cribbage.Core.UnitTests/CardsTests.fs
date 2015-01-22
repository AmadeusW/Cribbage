namespace Cribbage.Core.UnitTests

open NUnit.Framework
open Cribbage.Core

[<TestFixture>]
module Cards =

    open Cribbage.Core.Cards

    module FullDeckTests =

        [<Test>]
        let ``deck has 52 cards`` ()=
            Assert.AreEqual((Cards.deck |> List.length), 52)

        [<Test>]
        let ``deck contains the same number of cards with each suit`` ()=
            // Arrange
            let getSuit card =
                match card with
                | RankCard (_, suit) -> suit
                | FaceCard (_, suit) -> suit
            let suits = Cards.deck
                        |> List.map getSuit 
                        |> Seq.groupBy (fun s -> s)
                        |> Seq.map (fun (k, g) -> Seq.length g)
                        |> Seq.toList

            // Act
            // Assert
            Assert.AreEqual(suits |> List.max, suits |> List.min)

        [<Test>]
        let ``deck contains the same number of cards with each rank and face`` ()=
            // Arrange
            let cardToFaceOrRankString card = 
                match card with
                | RankCard (r, _) -> rankToString r
                | FaceCard (f, _) -> faceToString f

            let values = Cards.deck
                         |> List.map cardToFaceOrRankString
                         |> Seq.groupBy (fun s -> s)
                         |> Seq.map (fun (k, g) -> Seq.length g)
                         |> Seq.toList

            // Act
            // Assert
            Assert.AreEqual(values |> List.max, values |> List.min)

    module DealTwoHandsTests =

        let dealCardsAndAssertHandsAndDeck deck nbOfCards =
            // Arrange
            let nbOfCardsInTheDeck = deck |> List.length
            let expectedNbOfCardsInDeckAfterDeal = nbOfCardsInTheDeck - (2 * nbOfCards)
            // Act
            let hand1, hand2, deck = Cards.dealTwoHands deck nbOfCards
            // Assert
            Assert.AreEqual((hand1 |> List.length), nbOfCards)
            Assert.AreEqual((hand1 |> List.length), nbOfCards)
            Assert.AreEqual((deck |> List.length), expectedNbOfCardsInDeckAfterDeal)

        let tenCardDeck = deck |> List.take 10
        let dealCardsFromTenCardDeckAndAssert = dealCardsAndAssertHandsAndDeck tenCardDeck

        [<Test>]
        let ``Deal two 5 card hands from 10 cards deck should return two 5 card hands and empty dec`` ()=
            dealCardsFromTenCardDeckAndAssert 5
    
        [<Test>]
        let ``Deal two 3 card hands from 10 cards deck should return two 3 card hands and deck of 4 cards`` ()=
            dealCardsFromTenCardDeckAndAssert 3

        [<Test>]
        let ``Deal two 0 card hands from 10 cards deck should return two 0 card hands and deck of 10 cards`` ()=
            dealCardsFromTenCardDeckAndAssert 0

        [<Test>]
        [<ExpectedException(typeof<System.ArgumentException>)>]
        let ``Deal two 6 card hands from 10 cards deck should throw invalidArg`` ()=
            dealCardsFromTenCardDeckAndAssert 6

        let sevenCardDeck = deck |> List.take 7
        let dealCardsFromSevenCardDeckAndAssert = dealCardsAndAssertHandsAndDeck sevenCardDeck

        [<Test>]
        let ``Deal two 3 card hands from 7 cards deck should return two 3 card hands and deck of 1 cards`` ()=
            dealCardsFromSevenCardDeckAndAssert 3

        [<Test>]
        [<ExpectedException(typeof<System.ArgumentException>)>]
        let ``Deal two 6 card hands from 7 cards deck should throw invalidArg`` ()=
            dealCardsFromSevenCardDeckAndAssert 4
