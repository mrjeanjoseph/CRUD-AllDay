﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using VillaAG.Main.Controllers;

namespace VillaAG.Main.Tests.Controllers {
    [TestClass]
    public class HomeControllerTest {
        [TestMethod]
        public void Index() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
