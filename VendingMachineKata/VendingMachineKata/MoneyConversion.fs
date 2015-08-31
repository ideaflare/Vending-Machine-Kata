namespace VendingMachineKata

open ObjectTypes

module MoneyConversion =
    
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

