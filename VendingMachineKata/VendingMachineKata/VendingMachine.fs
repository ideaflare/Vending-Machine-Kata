namespace VendingMachineKata

type Coin = Penny | Dime | Nickel | Quarter
type Product = Cola | Chips | Candy
type State = Initial | Credit | Thanks

type VendingMachine() = 
    
    let mutable state = Initial
    let mutable credit = 0
    let mutable returnedCoins = []

    let coinValue coin = 
        match coin with
        | Penny -> 1
        | Dime -> 5
        | Nickel -> 10
        | Quarter -> 25

    let productValue product =
        match product with
        | Cola -> 100
        | Chips -> 50
        | Candy -> 65

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
        if credit >= (productValue product)
            then
                state <- Thanks
                Some(product)
            else 
                state <- Initial
                None

    member this.Display
        with get() =
            match state with
            | Initial -> "INSERT COIN"
            | Credit -> string credit
            | Thanks -> "THANK YOU"

    member this.TakeCoinReturn
        with get() =
            let change = returnedCoins
            returnedCoins <- []
            change

    
                    

//    cola for $1.00, chips for $0.50, and candy for $0.65