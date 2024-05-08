using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain;
using SportsStore.Web.Controllers;
using SportsStore.Web.Models;
using System.Linq;
using System.Web.Mvc;

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

        //Testing the Cart functionality
        [TestMethod]
        public void CanAddToCart()
        {
            //Arrange - Create the moc repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { ProductId = 1, Name = "Product One", Category = "Softwares"},
                new Product { ProductId = 2, Name = "Product Two", Category = "Hardwares"},
            }.AsQueryable());

            //Arrange and create the cart
            Cart cart = new Cart();

            //Arrange and creating the controller
            CartController target = new CartController(mock.Object);

            //Act - Add a product to the cart
            target.AddToCart(cart, 1, null);

            //Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductId, 1);
        }

        [TestMethod]
        public void CanAddToCartThenToCartScreen()
        {
            //Arrange - Create the moc repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { ProductId = 1, Name = "Product One", Category = "Softwares"},
                new Product {   ProductId = 2, Name = "Product Two", Category = "Hardwares"},
            }.AsQueryable());

            //Arrange and create the cart
            Cart cart = new Cart();

            //Arrange and creating the controller
            CartController target = new CartController(mock.Object);

            //Act - Add a product to the cart
            RedirectToRouteResult result = target.AddToCart(cart, 2, "myurl");

            //Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myurl");
        }

        [TestMethod]
        public void CanViewCartContents()
        {
            //Arrange and create the cart
            Cart cart = new Cart();

            //Arrange and creating the controller
            CartController target = new CartController(null);

            //Act - call the index action method
            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myurl").ViewData.Model;

            //Assert
            Assert.AreEqual(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myurl");
        }
    }
}
