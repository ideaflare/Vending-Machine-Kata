namespace VendingMachineKata

type Coin = Penny | Dime | Nickel | Quarter

type VendingMachine() = 
    
    member this.Insert coin =
        match coin with
        | Penny -> false
        | _ -> true
        
