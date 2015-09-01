namespace VendingMachineKata

open ObjectTypes
open MoneyConversion

type State = Initial | Credit | Thanks | MoreCoins | Price | SoldOut

type VendingMachine(colas, chips, candies) = 
    
    let mutable state = Initial
    let mutable credit = 0
    let mutable insertedCoins = []
    let mutable returnedCoins = []
    let mutable priceDisplay = ""

    let stock = 
        let inventory = [Cola, colas; Chips, chips; Candy, candies]
        System.Linq.Enumerable.ToDictionary(inventory, fst, snd)

    let purchase product =
        let price = productValue product
        if credit >= price then
                state <- Thanks
                returnedCoins <- returnedCoins @ getCoins (credit - price)
                credit <- 0
                stock.[product] <- stock.[product] - 1
                Some(product)
            else 
                state <- Price
                priceDisplay <- sprintf "PRICE $%d.%02d" (price / 100) (price % 100)
                None

    new() = VendingMachine(10,10,10)

    member this.Insert coin =
        match coin with
        | Penny -> 
            returnedCoins <- coin :: returnedCoins
            false
        | _ -> 
            insertedCoins <- coin :: insertedCoins
            credit <- credit + (coinValue coin)
            state <- Credit
            true
            
    member this.Purchase product =
        if stock.[product] < 1 then 
                state <- SoldOut
                None
        else purchase product

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
            | SoldOut ->
                if credit > 0
                    then state <- Credit
                    else state <- Initial
                "SOLD OUT"

    member this.TakeCoinReturn
        with get() =
            let change = returnedCoins
            returnedCoins <- []
            change

    member this.ReturnCoins =
            returnedCoins <- returnedCoins @ insertedCoins
            insertedCoins <- []
            credit <- 0
            state <- Initial
