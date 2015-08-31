namespace VendingMachineKataTests
open Microsoft.VisualStudio.TestTools.UnitTesting
open VendingMachineKata

[<TestClass>]
type VendingMachineTests() =
    
    let mutable vm = new VendingMachine()
    [<TestInitialize>]
    member this.``New vending machine for each test``()=
        vm <- new VendingMachine()

    [<TestMethod>]
    member this.``Valid Coins are accepted``()=
        Assert.IsTrue(vm.Insert Dime)
        Assert.IsTrue(vm.Insert Nickel)
        Assert.IsTrue(vm.Insert Quarter)

    [<TestMethod>]
    member this.``Pennies are rejected``()=
        Assert.IsFalse(vm.Insert Penny)

    [<TestMethod>]
    member this.``Accepted coins updates display``()=
        Assert.AreEqual("INSERT COIN", vm.Display)
        vm.Insert Dime |> ignore
        Assert.AreEqual("5", vm.Display)
        vm.Insert Nickel |> ignore
        Assert.AreEqual("15", vm.Display)
        vm.Insert Quarter |> ignore
        Assert.AreEqual("40", vm.Display)

    [<TestMethod>]
    member this.``Rejected coins are returned``()=
        vm.Insert(Penny) |> ignore
        vm.Insert(Penny) |> ignore
        let returnedCoins = vm.TakeCoinReturn
        Assert.AreEqual([Penny;Penny;], returnedCoins)

