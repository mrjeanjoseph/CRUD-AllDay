using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Tests {
    [TestClass]
    public class AdminTests {

        [TestMethod]
        public void IndexContainsAllProducts() {

            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product(){ProductID=1, Name="Product1"},
                new Product(){ProductID=1, Name="Product2"},
                new Product(){ProductID=1, Name="Product3"},
                }.AsQueryable()
           );

            AdminController target = new AdminController(mock.Object);

            IQueryable<Product> product = (IQueryable<Product>)target.Index().Model;
            Product[] products = product.ToArray();

            Assert.AreEqual(3, products.Length);
            Assert.AreEqual("Product1", products[0].Name);
            Assert.AreEqual("Product2", products[1].Name);
            Assert.AreEqual("Product3", products[2].Name);
        }
    }
}
