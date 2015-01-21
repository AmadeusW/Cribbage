module Cards

    // declare ranks (Ace, 2 - 10) and faces (JQK)
    type Rank = Ace | Two | Three | Four | Five | Six | Seven | Eight | Nine | Ten
    type Face = Jack | Queen | King

    let ranks = [ Ace ; Two ; Three ; Four ; Five ; Six ; Seven ; Eight ; Nine ; Ten ]
    let faces = [ Jack ; Queen ; King ]

    // declare suits
    type Suit = Spade | Heart | Diamond | Club
    let suits = [ Spade ; Heart ; Diamond ; Club ]

    // declare Card
    type Card =
    | RankCard of (Rank * Suit)
    | FaceCard of Face * Suit

    // declare how much the cards are worth
    let rankToPoints card =
        match card with
        | RankCard (rank, _) ->
            match rank with
            | Ace -> 1
            | Two -> 2
            | Three -> 3
            | Four -> 4
            | Five -> 5
            | Six -> 6
            | Seven -> 7
            | Eight -> 8
            | Nine -> 9
            | Ten -> 10
        | FaceCard (face, _) -> 10

    // declare how to print a card to the screen
    let rankToString card = 
        match card with
        | Ace -> "A"
        | x -> string (rankToPoints (RankCard (x, Diamond)))

    let faceToString card =
        match card with
        | Jack -> "J"
        | Queen -> "Q"
        | King -> "K"

    let suitToString suit =
        match suit with
        | Spade -> "â "
        | Heart -> "âĽ"
        | Diamond -> "âŚ"
        | Club -> "âŁ"

    let cardToString card =
        match card with
        | RankCard (rank, suit) -> (rankToString rank) + (suitToString suit)
        | FaceCard (face, suit) -> (faceToString face) + (suitToString suit)

    // declare collections of rank/face cards
    let rankCards =
        [ for rank in ranks
          -> [ for suit in suits
                -> RankCard (rank, suit) ] ] |> List.concat
    
    let faceCards =
        [ for face in faces
          -> [ for suit in suits
               -> FaceCard (face, suit) ] ] |> List.concat

    // concatenate collections of rank/face cards to form a proper deck
    let deck = rankCards @ faceCards

    // a set of cards form a hand
    type Hand = Card list

    open System
    let rnd = new Random()

    // shuffle the deck
    let shuffle deck =
        deck
        |> List.map (fun c -> (c, rnd.NextDouble()))
        |> List.sortBy snd
        |> List.map fst

    // deal a set of hands, each with given number of cards
    let dealTwoHands deck nbOfCards =
        let rec deal deck firstHand secondHand nbOfCards =
            match nbOfCards with
            | 0 -> firstHand, secondHand, deck
            | _ -> 
                match deck with
                | first :: second :: newDeck -> deal newDeck (first :: firstHand) (second :: secondHand) (nbOfCards - 1)
                | _ -> invalidArg "nbOfCards" "Can't deal the hands, not enough cards"
        deal deck [] [] nbOfCards