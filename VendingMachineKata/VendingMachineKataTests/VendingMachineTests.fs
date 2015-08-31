namespace VendingMachineKataTests
open Microsoft.VisualStudio.TestTools.UnitTesting
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
    member this.``Only Valid Coins are accepted``()=
        Assert.IsFalse(vm.Insert Penny)
        Assert.IsTrue(vm.Insert Dime)
        Assert.IsTrue(vm.Insert Nickel)
        Assert.IsTrue(vm.Insert Quarter)

    [<TestMethod>]
    member this.``Accepted coins updates display``()=
        Assert.AreEqual("INSERT COIN", vm.Display)
        insert Dime
        Assert.AreEqual("5", vm.Display)
        insert Nickel
        Assert.AreEqual("15", vm.Display)
        insert Quarter
        Assert.AreEqual("40", vm.Display)

    [<TestMethod>]
    member this.``Rejected coins are returned``()=
        insert Penny
        insert Penny
        let returnedCoins = vm.TakeCoinReturn
        Assert.AreEqual([Penny;Penny;], returnedCoins)

