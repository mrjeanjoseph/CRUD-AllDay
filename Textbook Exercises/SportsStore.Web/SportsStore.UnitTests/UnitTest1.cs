using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain;
using SportsStore.Web.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanPaginate()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { ProductId =  5, Name  = "Product 1"},
                new Product { ProductId =  10, Name  = "Product 2"},
                new Product { ProductId =  15, Name  = "Product 3"},
                new Product { ProductId =  20, Name  = "Product 4"},
                new Product { ProductId =  25, Name  = "Product 5"},
            });

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            IEnumerable<Product> result =
                (IEnumerable<Product>)controller.ProductListing(2).Model;

            //Assert
            Product[] productArray = result.ToArray();
            Assert.IsTrue(productArray.Length == 2);
            Assert.AreEqual(productArray[0].Name, "Product 4");
            Assert.AreEqual(productArray[1].Name, "Product 5");
        }
    }
}
