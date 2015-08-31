namespace VendingMachineKataTests
open Microsoft.VisualStudio.TestTools.UnitTesting
open VendingMachineKata.ObjectTypes
open VendingMachineKata

[<TestClass>]
type VendingMachineTests() =
    
    let mutable vm = new VendingMachine()
    let insert coin =
        vm.Insert coin |> ignore

    [<TestInitialize>]
    member this.``New vending machine for each test``()=
        vm <- new VendingMachine()

    [<TestMethod>]
    member this.``Coins accepted only if valid``()=
        Assert.IsFalse(vm.Insert Penny)
        Assert.IsTrue(vm.Insert Dime)
        Assert.IsTrue(vm.Insert Nickel)
        Assert.IsTrue(vm.Insert Quarter)

    [<TestMethod>]
    member this.``Coins accepted updates display``()=
        Assert.AreEqual("INSERT COIN", vm.Display)
        insert Dime
        Assert.AreEqual("5", vm.Display)
        insert Nickel
        Assert.AreEqual("15", vm.Display)
        insert Quarter
        Assert.AreEqual("40", vm.Display)

    [<TestMethod>]
    member this.``Coins rejected are returned``()=
        insert Penny
        insert Penny
        let returnedCoins = vm.TakeCoinReturn
        Assert.AreEqual([Penny;Penny;], returnedCoins)

    [<TestMethod>]
    member this.``Purchase thanks customer``()=
        insert Quarter
        insert Quarter
        let product = vm.Purchase(Chips)
        Assert.AreEqual(Some(Chips), product)
        Assert.AreEqual("THANK YOU", vm.Display)

    [<TestMethod>]
    member this.``Purchase resets credit``()=
        this.``Purchase thanks customer``()
        Assert.AreEqual("INSERT COINS", vm.Display)
        insert Dime
        Assert.AreEqual("5", vm.Display)

    [<TestMethod>]
    member this.``Purchase without enough credit displays price``()=
        let product = vm.Purchase(Cola)
        Assert.AreEqual("PRICE $1.00", vm.Display);
        Assert.AreEqual("INSERT COINS", vm.Display);
        insert Nickel
        let product = vm.Purchase(Candy)
        Assert.AreEqual("PRICE $0.65", vm.Display);
        Assert.AreEqual("10", vm.Display);

    [<TestMethod>]
    member this.``Change after purchase is returned``()=
        insert Quarter
        insert Quarter
        insert Nickel
        insert Dime
        let product = vm.Purchase(Chips)
        let change = vm.TakeCoinReturn
        Assert.AreEqual([Nickel;Dime;], change);

    [<TestMethod>]
    member this.``ReturnCoins returns inserted coins``()=
        insert Dime
        insert Quarter
        insert Quarter
        vm.ReturnCoins
        let returnedCoins = vm.TakeCoinReturn
        Assert.AreEqual([Quarter;Quarter;Dime], returnedCoins)
        Assert.AreEqual("INSERT COIN", vm.Display)
        insert Dime
        Assert.AreEqual("5", vm.Display)

    [<TestMethod>]
    member this.``Sold out stock notifies customer``()=
        vm <- new VendingMachine(colas=0,chips=0,candies=0)
        let product = vm.Purchase(Cola)
        Assert.AreEqual("SOLD OUT", vm.Display)
        Assert.AreEqual("INSERT COIN", vm.Display)
        insert Nickel
        let product = vm.Purchase(Cola)
        Assert.AreEqual("SOLD OUT", vm.Display)
        Assert.AreEqual("10", vm.Display)

       



