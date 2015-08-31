namespace VendingMachineKata

type Coin = Penny | Dime | Nickel | Quarter

type VendingMachine() = 
    
    let mutable credit = 0
    let mutable returnedCoins = []

    let coinValue coin = 
        match coin with
        | Penny -> 1
        | Dime -> 5
        | Nickel -> 10
        | Quarter -> 25

    member this.Insert coin =
        match coin with
        | Penny -> 
            returnedCoins <- coin :: returnedCoins
            false
        | _ -> 
            credit <- credit + (coinValue coin)
            true
        
    member this.Display
        with get() =
            if credit > 0 then (string credit)
            else "INSERT COIN"

    member this.TakeCoinReturn
        with get() =
            let change = returnedCoins
            returnedCoins <- []
            change