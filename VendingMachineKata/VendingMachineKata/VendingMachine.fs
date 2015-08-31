namespace VendingMachineKata

type Coin = Penny | Dime | Nickel | Quarter

type VendingMachine() = 
    
    let mutable amount = 0

    let coinValue coin = 
        match coin with
        | Penny -> 1
        | Dime -> 5
        | Nickel -> 10
        | Quarter -> 25

    member this.Insert coin =
        match coin with
        | Penny -> false
        | _ -> 
            amount <- amount + (coinValue coin)
            true
        
    member this.Display
        with get() =
            if amount > 0 then (string amount)
            else "INSERT COIN"