using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain;
using System;
using System.Linq;

//All passed
namespace PartOne.Tests.SportsStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void CanAddNewLine()
        {
            //Arrange - Create some test Merchandises
            Merchandise mOne = new Merchandise { Id = 1, Name = "Merch One" };
            Merchandise mTwo= new Merchandise { Id = 2, Name = "Merch Two" };

            //Arrange - Create a new cart
            Cart target = new Cart();

            //Act
            target.AddItem(mOne, 1);
            target.AddItem(mTwo, 1);
            CartLine[] results = target.Lines.ToArray();

            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Merchandise, mOne);
            Assert.AreEqual(results[1].Merchandise, mTwo);
        }

        [TestMethod]
        public void CanAddQtyForExistingLines()
        {
            //Arrange - Create some test Merchandises
            Merchandise mOne = new Merchandise { Id = 1, Name = "Merch One" };
            Merchandise mTwo = new Merchandise { Id = 2, Name = "Merch Two" };

            //Arrange - Create a new cart
            Cart target = new Cart();

            //Act
            target.AddItem(mOne, 1);
            target.AddItem(mTwo, 1);
            target.AddItem(mOne, 10);
            CartLine[] results = target.Lines.OrderBy(c => c.Merchandise.Id).ToArray();

            //Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 1);

        }

        [TestMethod]
        public void CanRemoveLines()
        {
            //Arrange - Create some test Merchandises
            Merchandise mOne = new Merchandise { Id = 1, Name = "Merch One" };
            Merchandise mTwo = new Merchandise { Id = 2, Name = "Merch Two" };
            Merchandise mThree = new Merchandise { Id = 3, Name = "Merch Three" };

            //Arrange - Create a new cart
            Cart target = new Cart();
            // Add some merchandise to the cart
            target.AddItem(mOne, 1);
            target.AddItem(mTwo, 3);
            target.AddItem(mThree, 5);
            target.AddItem(mTwo, 1);

            //Act
            target.RemoveLine(mTwo);

            //Assert
            Assert.AreEqual(target.Lines.Where(c => c.Merchandise == mTwo).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }

        [TestMethod]
        public void CalculateCartTotal()
        {
            //Arrange - Create some test Merchandises
            Merchandise mOne = new Merchandise { Id = 1, Name = "Merch One", Price = 100M };
            Merchandise mTwo = new Merchandise { Id = 2, Name = "Merch Two", Price = 150M };
            Merchandise mThree = new Merchandise { Id = 3, Name = "Merch Three", Price = 70M };

            //Arrange - Create a new cart
            Cart target = new Cart();
            // Add some merchandise to the cart
            target.AddItem(mOne, 1);
            target.AddItem(mTwo, 1);
            target.AddItem(mOne, 4);
            decimal result = target.ComputeTotalValue();

            //Assert
            Assert.AreEqual(result, 650M);
        }


        [TestMethod]
        public void CanClearContent()
        {
            //Arrange - Create some test Merchandises
            Merchandise mOne = new Merchandise { Id = 1, Name = "Merch One", Price = 100M };
            Merchandise mTwo = new Merchandise { Id = 2, Name = "Merch Two", Price = 150M };
            Merchandise mThree = new Merchandise { Id = 3, Name = "Merch Three", Price = 70M };

            //Arrange - Create a new cart
            Cart target = new Cart();
            // Add some merchandise to the cart
            target.AddItem(mOne, 1);
            target.AddItem(mTwo, 3);
            target.AddItem(mThree, 5);
            target.AddItem(mTwo, 1);

            //Act
            target.Clear();

            //Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }
    }
}
