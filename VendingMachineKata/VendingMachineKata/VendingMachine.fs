namespace VendingMachineKata

open ObjectTypes
open MoneyConversion

type State = Initial | Credit | Thanks | MoreCoins | Price

type VendingMachine() = 
    
    let mutable state = Initial
    let mutable credit = 0
    let mutable returnedCoins = []
    let mutable priceDisplay = ""

    member this.Insert coin =
        match coin with
        | Penny -> 
            returnedCoins <- coin :: returnedCoins
            false
        | _ -> 
            credit <- credit + (coinValue coin)
            state <- Credit
            true
            
    member this.Purchase product =
        let price = productValue product
        if credit >= price
            then
                state <- Thanks
                credit <- 0
                Some(product)
            else 
                state <- Price
                priceDisplay <- sprintf "PRICE $%d.%02d" (price / 100) (price % 100)
                None

    member this.Display
        with get() =
            match state with
            | Initial -> "INSERT COIN"
            | MoreCoins -> "INSERT COINS"
            | Credit -> string credit
            | Thanks -> 
                state <- MoreCoins
                "THANK YOU"
            | Price ->
                if credit > 0
                    then state <- Credit
                    else state <- MoreCoins
                priceDisplay

    member this.TakeCoinReturn
        with get() =
            let change = returnedCoins
            returnedCoins <- []
            change
