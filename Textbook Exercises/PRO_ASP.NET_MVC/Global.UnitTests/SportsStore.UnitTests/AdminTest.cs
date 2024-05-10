using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain;
using SportsStore.Web.Controllers;
using System.Collections.Generic;
using System.Linq;

//All passed
namespace PartOne.Tests.SportsStore.UnitTests
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void ContainsAllMerchandises()
        {
            // Arrange -- Create the mock repository
            Mock<IMerchandiseRepository> mock = new Mock<IMerchandiseRepository>();
            mock.Setup(m => m.Merchandises).Returns(new Merchandise[]
            {
                new Merchandise {Id = 5, Name = "Showy Evening Primrose"},
                new Merchandise {Id = 10, Name = "Mississippi River Wakerobin"},
                new Merchandise {Id = 15, Name = "Plectocarpon Lichen"},
                new Merchandise {Id = 20, Name = "Flaxleaf Pimpernel"},
                new Merchandise {Id = 25, Name = "Torrey's Penstemon"},
            });

            // Arrange -- Create a controller
            AdminController target = new AdminController(mock.Object);

            // Action 
            Merchandise[] result = ((IEnumerable<Merchandise>)target.Index().ViewData.Model).ToArray();

            // Assert
            Assert.AreEqual(result.Length, 5);
            Assert.AreEqual("Showy Evening Primrose", result[0].Name);
            Assert.AreEqual("Mississippi River Wakerobin", result[1].Name);
            Assert.AreEqual("Torrey's Penstemon", result[4].Name);
        }

        [TestMethod]
        public void CanEditAllMerchandises()
        {
            // Arrange -- Create the mock repository
            Mock<IMerchandiseRepository> mock = new Mock<IMerchandiseRepository>();
            mock.Setup(m => m.Merchandises).Returns(new Merchandise[]
            {
                new Merchandise {Id = 5, Name = "Showy Evening Primrose"},
                new Merchandise {Id = 10, Name = "Mississippi River Wakerobin"},
                new Merchandise {Id = 15, Name = "Plectocarpon Lichen"},
                new Merchandise {Id = 20, Name = "Flaxleaf Pimpernel"},
                new Merchandise {Id = 25, Name = "Torrey's Penstemon"},
            });

            // Arrange -- Create a controller
            AdminController target = new AdminController(mock.Object);

            // Action 
            Merchandise mOne = target.Edit(5).ViewData.Model as Merchandise;
            Merchandise mTwo = target.Edit(10).ViewData.Model as Merchandise;
            Merchandise mThree = target.Edit(15).ViewData.Model as Merchandise;

            // Assert
            Assert.AreEqual(5, mOne.Id);
            Assert.AreEqual(10, mTwo.Id);
            Assert.AreEqual(15, mThree.Id);
        }

        [TestMethod]
        public void ShouldNotEditNullMerchs()
        {
            // Arrange -- Create the mock repository
            Mock<IMerchandiseRepository> mock = new Mock<IMerchandiseRepository>();
            mock.Setup(m => m.Merchandises).Returns(new Merchandise[]
            {
                new Merchandise {Id = 5, Name = "Showy Evening Primrose"},
                new Merchandise {Id = 10, Name = "Mississippi River Wakerobin"},
                new Merchandise {Id = 15, Name = "Plectocarpon Lichen"},
                new Merchandise {Id = 20, Name = "Flaxleaf Pimpernel"},
                new Merchandise {Id = 25, Name = "Torrey's Penstemon"},
            });

            // Arrange -- Create a controller
            AdminController target = new AdminController(mock.Object);

            // Action 
            Merchandise result = target.Edit(4).ViewData.Model as Merchandise;

            // Assert
            Assert.IsNull(result);
        }
    }
}
