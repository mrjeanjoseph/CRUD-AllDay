using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain;
using SportsStore.Web.Controllers;
using System.Linq;
using System.Web.Mvc;

//All passed
namespace SportsStore.UnitTests
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void CanRetrieveImageData() 
        {
            // Arrnage - create a product with image data
            Merchandise merch = new Merchandise
            {
                Id = 2,
                Name = "Merch Two",
                ImageData = new byte[] { },
                ImageMimeType = "image/png",
            };

            // Arrange - create the controller
            Mock<IMerchandiseRepository> mock = new Mock<IMerchandiseRepository>();
            mock.Setup(m => m.Merchandises).Returns(new Merchandise[] { 
                new Merchandise { Id = 1, Name="Merch One"},
                merch,
                new Merchandise { Id = 3, Name = "Merch Three"},
            }.AsQueryable());

            // Arrange - create the controller
            MerchController target = new MerchController(mock.Object);

            // Act - call the GetImage action method
            ActionResult result = target.GetImage(2);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(merch.ImageMimeType, ((FileResult)result).ContentType);
        }
        [TestMethod]
        public void CannotRetrieveImageData() 
        {
            // Arrange - create the controller
            Mock<IMerchandiseRepository> mock = new Mock<IMerchandiseRepository>();
            mock.Setup(m => m.Merchandises).Returns(new Merchandise[] { 
                new Merchandise { Id = 1, Name="Merch One"},
                new Merchandise { Id = 2, Name = "Merch Two"},
            }.AsQueryable());

            // Arrange - create the controller
            MerchController target = new MerchController(mock.Object);

            // Act - call the GetImage action method
            ActionResult result = target.GetImage(100);

            // Assert
            Assert.IsNull(result);
        }
    }
}
