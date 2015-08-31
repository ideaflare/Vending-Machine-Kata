namespace VendingMachineKata

open ObjectTypes
open MoneyConversion

type State = Initial | Credit | Thanks

type VendingMachine() = 
    
    let mutable state = Initial
    let mutable credit = 0
    let mutable returnedCoins = []

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
