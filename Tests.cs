namespace Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Utilities;
using Applications;

[TestClass]
public class UnitTests
{
    // Test plan for task two:
    /*

    My solution to this task can be tested entirely through unit tests.

    */
    [TestMethod]
    public void KitchenTap() {
        decimal flowRate = 9;
        decimal minPerDay = 10;
        decimal pricePer1000L = 2.5m;


        decimal cost = WaterCostCalculator.CostPerDay(flowRate, minPerDay, pricePer1000L);
        Assert.AreEqual(0.23m, cost);
    }

    [TestMethod]
    public void WashingMachine() {
        decimal cycleVolume = 50;
        decimal cyclesPerDay = 3;
        decimal pricePer1000L = 2.5m;


        decimal cost = WaterCostCalculator.CostPerDay(cycleVolume, cyclesPerDay, pricePer1000L);
        Assert.AreEqual(0.38m, cost);
    }

    [TestMethod]
    public void Shower() {
        decimal flowRate = 5;
        decimal minPerDay = 12;
        decimal pricePer1000L = 2.99m;


        decimal cost = WaterCostCalculator.CostPerDay(flowRate, minPerDay, pricePer1000L);
        Assert.AreEqual(0.18m, cost);
    }

    // Test plan for task three:
    /*

    This requires manual testing due to IO use.
    Each test is conducted on the same instance of `Library` as its predecessors.
    Note that I included some books already in `library.json`, which the program reads from initially.

    Test case 1, list all books:
        Output:
            Three Body Problem by Cixin Liu
            The Motorcycle Diaries by Ernesto 'Che' Guevara
            Space Adventures by Ronnie Johnson
            Joe in Space by Aisha Khan
            The Dragons by Michelle Smith
            Press any key to continue...
        Result:
            Successful

    Test case 2, list all books in genre:
        Input:
            Science Fiction
        Output:
            Three Body Problem by Cixin Liu
            Space Adventures by Ronnie Johnson
            Joe in Space by Aisha Khan
            Press any key to continue...
        Result:
            Successful

    Test case 3, add new book:
        Input:
            Title: On Palestine
            Author(s): Noam Chomsky, Ilan Pappe
            Genre(s): Non-Fiction, History
        Result:
            Successful

    Test case 3.1, list all books again:
        Output:
            Three Body Problem by Cixin Liu
            The Motorcycle Diaries by Ernesto 'Che' Guevara
            Space Adventures by Ronnie Johnson
            Joe in Space by Aisha Khan
            The Dragons by Michelle Smith
            On Palestine by Noam Chomsky, Ilan Pappe
            Press any key to continue...
        Result:
            Successful
    */

    // Test plan for task four:
    /*

    My solution to this task can be tested entirely through unit tests.

    */
    [TestMethod]
    public void HexToDen()
    {
        var f = Validators.ValidateHexadecimal;

        {
            f("12C", out int d);
            Assert.AreEqual(300, d);
        }

        {
            f("37", out int d);
            Assert.AreEqual(55, d);
        }

        {
            f("28F", out int d);
            Assert.AreEqual(655, d);
        }

        {
            f("4B0", out int d);
            Assert.AreEqual(1200, d);
        }
    }

    [TestMethod]
    public void DenToHex()
    {
        var f = Validators.ValidateDenary;

        {
            f("300", out string h);
            Assert.AreEqual("12C", h);
        }

        {
            f("55", out string h);
            Assert.AreEqual("37", h);
        }

        {
            f("655", out string h);
            Assert.AreEqual("28F", h);
        }

        {
            f("1200", out string h);
            Assert.AreEqual("4B0", h);
        }
    }
}
