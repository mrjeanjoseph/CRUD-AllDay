using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain;
using SportsStore.Web.Controllers;
using SportsStore.Web.HtmlHelpers;
using SportsStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
            IEnumerable<Product> resultOld =
                (IEnumerable<Product>)controller.ProductListing(2).Model;
            // New Code
            ProductsListViewModel result = (ProductsListViewModel)controller.ProductListing(2).Model;

            //Assert
            Product[] productArrayOld = resultOld.ToArray();

            // New Code
            Product[] productArray = result.Products.ToArray();

            Assert.IsTrue(productArray.Length == 2);
            Assert.AreEqual(productArray[0].Name, "Product 4");
            Assert.AreEqual(productArray[1].Name, "Product 5");
        }

        [TestMethod]
        public void CanGeneratePageLinks()
        {
            // Arrange - define an HTML helper - we need to do this
            // in order to apply the extension method
            HtmlHelper strHelper = null;

            // Arrange - create PagingInfo data
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Arrange - set up the delegate using a lambda express
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            MvcHtmlString result = strHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert -- Test failed here. Moving on
            Assert.AreEqual(@"<a class=""btn btn-secondary"" href=""Page1"">1</a><a class=""btn btn-secondary btn-primary selected"" href=""Page2"">2</a><a class=""btn btn-secondary"" href=""Page3"">3</a>", 
                result.ToString());
        }

        [TestMethod]
        public void CanSendPaginationViewModel()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { ProductId =  5, Name  = "Product 1"},
                new Product { ProductId =  10, Name  = "Product 2"},
                new Product { ProductId =  15, Name  = "Product 3"},
                new Product { ProductId =  20, Name  = "Product 4"},
                new Product { ProductId =  25, Name  = "Product 5"},
            });

            // Arrange
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            ProductsListViewModel result = (ProductsListViewModel)controller.ProductListing(2).Model;

            // Assert
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
        }
    }
}
