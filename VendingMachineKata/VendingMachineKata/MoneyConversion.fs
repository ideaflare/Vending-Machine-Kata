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

    let changeToCoin cents coin =
        let coinValue = coinValue coin
        let coins = [for i in [1..(cents / coinValue)] do yield coin]
        let change = cents % coinValue
        (change, coins)

    let getCoins cents =
        let change, quarters = changeToCoin cents Quarter
        let change, nickels = changeToCoin change Nickel
        let change, dimes = changeToCoin change Dime
        quarters @ nickels @ dimes

