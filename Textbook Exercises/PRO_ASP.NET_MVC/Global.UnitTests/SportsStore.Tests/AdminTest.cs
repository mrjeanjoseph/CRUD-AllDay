using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain;
using SportsStore.Web.Controllers;
using SportsStore.Web.Infrastructure;
using SportsStore.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

//All passed
namespace SportsStore.UnitTests
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

        [TestMethod]
        public void CanSaveValidChanges()
        {
            // Arrange - create mock repository
            Mock<IMerchandiseRepository> mock = new Mock<IMerchandiseRepository>();
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Arrange - create a merchandise
            Merchandise merch = new Merchandise { Name = "Test" };

            // Act - try to save merchandise
            ActionResult result = target.Edit(merch);

            // Assert - check that the repository was called
            mock.Verify(m => m.SaveMerchandise(merch));
            // Assert - check the method result type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void CannotSaveInvalidChanges()
        {
            // Arrange - create mock repository
            Mock<IMerchandiseRepository> mock = new Mock<IMerchandiseRepository>();
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Arrange - create a merchandise
            Merchandise merch = new Merchandise { Name = "Test" };
            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");

            // Act - try to save merchandise
            ActionResult result = target.Edit(merch);

            // Assert - check that the repository was called
            mock.Verify(m => m.SaveMerchandise(It.IsAny<Merchandise>()), Times.Never());
            // Assert - check the method result type
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void CanDeleteValidMerchs()
        {
            // Arrange - create a Merchandise
            Merchandise merch = new Merchandise { Id = 4, Name = "Pye Kachiman" };

            // Arrange - create the mock repository
            Mock<IMerchandiseRepository> mock = new Mock<IMerchandiseRepository>();
            mock.Setup(m => m.Merchandises).Returns(new Merchandise[]
            {
                new Merchandise {Id = 5, Name = "Showy Evening Primrose"},
                new Merchandise {Id = 10, Name = "Mississippi River Wakerobin"},
                new Merchandise {Id = 15, Name = "Plectocarpon Lichen"},
            });

            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);

            // Act - delete the product
            target.Delete(merch.Id);

            // Assert - ensure that the repository delete method was
            // called with the correct Product
            mock.Verify(m => m.DeleteMerchandise(merch.Id));
        }

        [TestMethod]
        public void CanLoginWithValidCreds()
        {
            // Arrange - create a mock authentication provider
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);

            // Arrange - create the view model
            LoginViewModel model = new LoginViewModel
            {
                UserName = "admin",
                Password = "secret",
            };

            // Arrange - create the controller
            AccountController target = new AccountController(mock.Object);

            // Act - authenticate using valid credentials
            ActionResult result = target.Login(model, "/MyUrl");

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyUrl", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void CannotLoginWithInvalidCreds()
        {
            // Arrange - create a mock authentication provider
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("invaliduser", "invalidpass")).Returns(true);

            // Arrange - create the view model
            LoginViewModel model = new LoginViewModel
            {
                UserName = "invaliduser",
                Password = "invalidpass",
            };

            // Arrange - create the controller
            AccountController target = new AccountController(mock.Object);

            // Act - authenticate using valid credentials
            ActionResult result = target.Login(model, "/MyUrl");

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid); // Action or View Result??
        }
    }
}
