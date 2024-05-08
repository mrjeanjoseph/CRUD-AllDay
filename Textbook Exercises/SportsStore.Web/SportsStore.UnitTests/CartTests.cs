using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain;
using System.Linq;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void CanAddNewLine()
        {
            //Arrange - Create a couple of test products
            Product pOne = new Product { ProductId = 1, Name = "Product One" };
            Product pTwo = new Product { ProductId = 2, Name = "Product Two" };

            //Arrange - Create a new cart
            Cart target = new Cart();

            //Act
            target.AddItem(pOne, 1);
            target.AddItem(pTwo, 1);
            CartLine[] results = target.Lines.ToArray();

            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, pOne);
            Assert.AreEqual(results[1].Product, pTwo);
        }

        [TestMethod]
        public void CanAddQtyForExistingLines()
        {
            //Arrange - Create a couple of test products
            Product pOne = new Product { ProductId = 1, Name = "Product One" };
            Product pTwo = new Product { ProductId = 2, Name = "Product Two" };

            //Arrange - Create a new cart
            Cart target = new Cart();

            //Act
            target.AddItem(pOne, 1);
            target.AddItem(pTwo, 1);
            target.AddItem(pOne, 10);
            CartLine[] results = target.Lines.OrderBy(c => c.Product.ProductId).ToArray();

            //Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 1);

        }

        [TestMethod]
        public void CanRemoveLines()
        {
            //Arrange - Create a couple of test products
            Product pOne = new Product { ProductId = 1, Name = "Product One" };
            Product pTwo = new Product { ProductId = 2, Name = "Product Two" };
            Product pThree = new Product { ProductId = 3, Name = "Product Three" };

            //Arrange - Create a new cart
            Cart target = new Cart();
            // Add some Product to the cart
            target.AddItem(pOne, 1);
            target.AddItem(pTwo, 3);
            target.AddItem(pThree, 5);
            target.AddItem(pTwo, 1);

            //Act
            target.RemoveItem(pTwo);

            //Assert
            Assert.AreEqual(target.Lines.Where(c => c.Product == pTwo).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }

        [TestMethod]
        public void CalculateCartTotal()
        {
            //Arrange - Create a couple of test products
            Product pOne = new Product { ProductId = 1, Name = "Product One", Price = 100M };
            Product pTwo = new Product { ProductId = 2, Name = "Product Two", Price = 150M };
            Product pThree = new Product { ProductId = 3, Name = "Product Three", Price = 70M };

            //Arrange - Create a new cart
            Cart target = new Cart();
            // Add some Product to the cart
            target.AddItem(pOne, 1);
            target.AddItem(pTwo, 1);
            target.AddItem(pOne, 4);
            decimal result = target.ComputeTotalValue();

            //Assert
            Assert.AreEqual(result, 650M);
        }

        [TestMethod]
        public void CanClearContent()
        {
            //Arrange - Create some test Product
            Product pOne = new Product { ProductId = 1, Name = "Product One", Price = 100M };
            Product pTwo = new Product { ProductId = 2, Name = "Product Two", Price = 150M };
            Product pThree = new Product { ProductId = 3, Name = "Product Three", Price = 70M };

            //Arrange - Create a new cart
            Cart target = new Cart();
            // Add some Product to the cart
            target.AddItem(pOne, 1);
            target.AddItem(pTwo, 3);
            target.AddItem(pThree, 5);
            target.AddItem(pTwo, 1);

            //Act
            target.Clear();

            //Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }

    }
}
