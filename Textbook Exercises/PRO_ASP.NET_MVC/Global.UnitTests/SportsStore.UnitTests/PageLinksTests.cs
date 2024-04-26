using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Web.Controllers;
using SportsStore.Web.HtmlHelpers;
using System.Collections.Generic;
using SportsStore.Web.Models;
using SportsStore.Domain;
using System.Web.Mvc;
using System.Linq;
using System;
using Moq;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class PageLinksTests
    {
        [TestMethod]
        public void CanPaginate()
        {
            // Arrange
            Mock<IMerchandiseRepository> mock = new Mock<IMerchandiseRepository>();
            mock.Setup(m => m.Merch).Returns(new Merchandise[]
            {
                new Merchandise {Id = 5, Name = "Showy Evening Primrose"},
                new Merchandise {Id = 10, Name = "Mississippi River Wakerobin"},
                new Merchandise {Id = 15, Name = "Plectocarpon Lichen"},
                new Merchandise {Id = 20, Name = "Flaxleaf Pimpernel"},
                new Merchandise {Id = 25, Name = "Torrey's Penstemon"},
            });

            MerchController controller = new MerchController(mock.Object);
            controller.PageSize = 3;

            // Act
            //IEnumerable<Merchandise> result = (IEnumerable<Merchandise>)controller.List(2).Model;
            MerchListViewModel result = (MerchListViewModel)controller.List(null, 2).Model;

            // Assert
            //Merchandise[] prodArray = result.ToArray();
            Merchandise[] prodArray = result.Merchandises.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            //Assert.AreEqual(prodArray[0].Id, 5);
            Assert.AreEqual(prodArray[0].Name, "Torrey's Penstemon");
            Assert.AreEqual(prodArray[1].Name, "Torrey's Penstemon");
        }

        [TestMethod]
        public void CanGeneratePageLinks()
        {
            // Arrange - define an HTML helper
            HtmlHelper testHelper = null;

            // Arrange - Create PagingInfo data
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            //Arrange - Set up the delegate using a lambda expression
            Func<int, string> pageurldel = i => "Page" + i;

            // Act
            MvcHtmlString result = testHelper.PageLinks(pagingInfo, pageurldel);

            //Assert
            Assert.AreEqual(@"<a class=""btn btn-secondary"" href=""Page1"">1</a>" +
                @"<a class=""btn btn-secondary btn-primary selected"" href=""Page2"">2</a>" +
                @"<a class=""btn btn-secondary"" href=""Page3"">3</a>", result.ToString());
        }

        [TestMethod]
        public void CanSendPaginationViewModel()
        {
            // Arrange
            Mock<IMerchandiseRepository> mock = new Mock<IMerchandiseRepository>();
            mock.Setup(m => m.Merch).Returns(new Merchandise[]
            {
                new Merchandise {Id = 5, Name = "Showy Evening Primrose"},
                new Merchandise {Id = 10, Name = "Mississippi River Wakerobin"},
                new Merchandise {Id = 15, Name = "Plectocarpon Lichen"},
                new Merchandise {Id = 20, Name = "Flaxleaf Pimpernel"},
                new Merchandise {Id = 25, Name = "Torrey's Penstemon"},
            });

            //Arrange
            MerchController controller = new MerchController(mock.Object);
            controller.PageSize = 3;

            //Act
            MerchListViewModel result = (MerchListViewModel)controller.List(null, 2).Model;

            //Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalItems, 2);
        }

        [TestMethod]
        public void CanFilterProducts()
        {
            // Arrange //Create the mock repo
            Mock<IMerchandiseRepository> mock = new Mock<IMerchandiseRepository>();
            mock.Setup(m => m.Merch).Returns(new Merchandise[]
            {
                new Merchandise {Id = 5, Name = "Showy Evening Primrose"},
                new Merchandise {Id = 10, Name = "Mississippi River Wakerobin"},
                new Merchandise {Id = 15, Name = "Plectocarpon Lichen"},
                new Merchandise {Id = 20, Name = "Flaxleaf Pimpernel"},
                new Merchandise {Id = 25, Name = "Torrey's Penstemon"},
            });

            //Arrange
            MerchController controller = new MerchController(mock.Object);
            controller.PageSize = 3;

            //Act
            Merchandise[] result = ((MerchListViewModel)controller
                .List("HVAC", 1).Model).Merchandises
                .ToArray();

            //Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "Showy Evening Primrose" && result[0].Category == "Temp Fencing, Decorative Fencing and Gates");
            Assert.IsTrue(result[1].Name == "Mississippi River Wakerobin" && result[0].Category == "Asphalt Paving");
        }

        [TestMethod]
        public void CanCreateCategories()
        {
            // Arrange //Create the mock repo
            Mock<IMerchandiseRepository> mock = new Mock<IMerchandiseRepository>();
            mock.Setup(m => m.Merch).Returns(new Merchandise[]
            {
                new Merchandise {Id = 5, Name = "Showy Evening Primrose", Category = "Interior"},
                new Merchandise {Id = 10, Name = "Mississippi River Wakerobin", Category = "Exterior"},
                new Merchandise {Id = 15, Name = "Plectocarpon Lichen", Category = "Interior"},
                new Merchandise {Id = 20, Name = "Flaxleaf Pimpernel", Category = "Franchise"},
                new Merchandise {Id = 25, Name = "Torrey's Penstemon", Category = "Exterior"},
            });

            //Arrange
            NavController target = new NavController(mock.Object);

            //Act = get the set of categories
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();

            //Assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Interior");
            Assert.AreEqual(results[1], "Exterior");
            Assert.AreEqual(results[2], "Franchise");
        }

        [TestMethod]
        public void IndicateSelectedCategory()
        {
            //Arrage - create the mock repository
            Mock<IMerchandiseRepository> mock = new Mock<IMerchandiseRepository>();
            mock.Setup(m => m.Merch).Returns(new Merchandise[]
            {
                new Merchandise {Id = 1, Name = "P1", Category = "Apples"},
                new Merchandise {Id = 4, Name = "P2", Category = "Oranges"},
            });

            //Arrage - Create the controller
            NavController target = new NavController(mock.Object);

            //Arrage - define the category to selected
            string categoryToSelect = "Apples";

            //Action
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            //Assert
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void GenerateCategorySpecificCount()
        {
            //Arrage - create the mock repository
            Mock<IMerchandiseRepository> mock = new Mock<IMerchandiseRepository>();
            mock.Setup(m => m.Merch).Returns(new Merchandise[]
            {
                new Merchandise {Id = 1, Name = "P1", Category = "Cat1"},
                new Merchandise {Id = 2, Name = "P2", Category = "Cat2"},
                new Merchandise {Id = 3, Name = "P3", Category = "Cat1"},
                new Merchandise {Id = 4, Name = "P4", Category = "Cat2"},
                new Merchandise {Id = 5, Name = "P5", Category = "Cat3"},
            });

            //Arrage - Create the controller and make the page size 3 times
            MerchController target = new MerchController(mock.Object);
            target.PageSize = 3;

            //Action - test the product counts for different categories
            int resultOne = ((MerchListViewModel)target.List("Cat1").Model).PagingInfo.TotalItems;
            int resultTwo = ((MerchListViewModel)target.List("Cat2").Model).PagingInfo.TotalItems;
            int resultThree = ((MerchListViewModel)target.List("Cat3").Model).PagingInfo.TotalItems;
            int resultAll = ((MerchListViewModel)target.List(null).Model).PagingInfo.TotalItems;

            //Assert
            Assert.AreEqual(resultOne, 2);
            Assert.AreEqual(resultTwo, 2);
            Assert.AreEqual(resultThree, 1);
            Assert.AreEqual(resultAll, 5);

        }
    }
}
