namespace VendingMachineKata

type Coin = Penny | Dime | Nickel | Quarter

type VendingMachine() = 
    
    member this.Insert coin =
        true
        
