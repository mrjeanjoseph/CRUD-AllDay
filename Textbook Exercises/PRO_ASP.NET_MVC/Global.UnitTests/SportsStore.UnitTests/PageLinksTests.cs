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

namespace SportsStore.UnitTests {
    [TestClass]
    public class PageLinksTests {
        [TestMethod]
        public void CanPaginate() {
            // Arrange
            Mock<IMerchRepo> mock = new Mock<IMerchRepo>();
            mock.Setup(m => m.Merch).Returns(new Merchandise[]
            {
                new Merchandise {Id = 5, Name = "Showy Evening Primrose"},
                new Merchandise {Id = 10, Name = "Mississippi River Wakerobin"},
                new Merchandise {Id = 15, Name = "Plectocarpon Lichen"},
                new Merchandise {Id = 20, Name = "Flaxleaf Pimpernel"},
                new Merchandise {Id = 25, Name = "Torrey's Penstemon"},
            });

            MerchController controller = new MerchController(mock.Object);
            controller.PageSize = 5;

            // Act
            IEnumerable<Merchandise> result = (IEnumerable<Merchandise>)controller.List(2).Model;

            // Assert
            Merchandise[] prodArray = result.ToArray();
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
    }
}
