using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Tests {

    [TestClass]
    public class CartsTests {

        [TestMethod]
        public void CanAddNewLines() {

            //add the product for the first time
            Cart target = new Cart();
            target.AddItem(new Product { ProductID = 1, Name = "productone", Category = "cartone", Price = 10 }, 10);

            CartLine[] actual = ((List<CartLine>)target.Lines).ToArray();
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(1, actual[0].Product.ProductID);
        }

        [TestMethod]
        public void CanAddQuantityToTheExistingLines() {

            Cart target = new Cart();
            target.AddItem(new Product { ProductID = 1, Name = "productone", Category = "cartone", Price = 10 }, 10);
            target.AddItem(new Product { ProductID = 1, Name = "productone", Category = "cartone", Price = 10 }, 10);

            CartLine[] actual = ((List<CartLine>)target.Lines).ToArray();
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(20, actual[0].Quantity);
        }

        [TestMethod]
        public void RemoveLine() {

            Cart target = new Cart();
            target.AddItem(new Product { ProductID = 1, Name = "productone", Category = "cartone", Price = 10 }, 10);
            target.AddItem(new Product { ProductID = 2, Name = "product-two", Category = "cartone", Price = 10 }, 10);
            target.AddItem(new Product { ProductID = 3, Name = "product-three", Category = "cartone", Price = 10 }, 10);
            target.RemoveLine(new Product { ProductID = 3, Name = "product-three", Category = "cartone", Price = 10 });
            CartLine[] actual = ((List<CartLine>)target.Lines).ToArray();
            Assert.AreEqual(2, actual.Length);
        }

        [TestMethod]
        public void CalculateCartTotal() {

            Cart target = new Cart();
            target.AddItem(new Product { ProductID = 1, Name = "productone", Category = "cartone", Price = 10 }, 10);
            target.AddItem(new Product { ProductID = 2, Name = "product-two", Category = "cartone", Price = 20 }, 20);
            target.AddItem(new Product { ProductID = 3, Name = "product-three", Category = "cartone", Price = 30 }, 30);
            decimal actual = target.ComputeTotalValue();
            Assert.AreEqual(1400M, actual);
        }

        [TestMethod]
        public void CanClearContents() {

            Cart target = new Cart();
            target.AddItem(new Product { ProductID = 1, Name = "productone", Category = "cartone", Price = 10 }, 10);
            target.AddItem(new Product { ProductID = 2, Name = "product-two", Category = "cartone", Price = 10 }, 10);
            target.AddItem(new Product { ProductID = 3, Name = "product-three", Category = "cartone", Price = 10 }, 10);
            target.Clear();

            Assert.AreEqual(0, target.Lines.Count());
        }
    }
}
