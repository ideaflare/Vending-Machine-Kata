namespace VendingMachineKataTests
open Microsoft.VisualStudio.TestTools.UnitTesting
open VendingMachineKata

[<TestClass>]
type VendingMachineTests() =
    
    [<TestMethod>]
    member this.``Valid Coins are accepted``()=
        let vm = new VendingMachine()
        Assert.IsTrue(vm.Insert Dime)
        Assert.IsTrue(vm.Insert Nickel)
        Assert.IsTrue(vm.Insert Quarter)

    [<TestMethod>]
    member this.``Pennies are rejected``()=
        let vm = new VendingMachine()
        Assert.IsFalse(vm.Insert Penny)
