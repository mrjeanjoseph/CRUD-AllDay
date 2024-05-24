using Chapter17.ControllersAndActions.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace PartOne.Tests.Chapter17.Tests
{
    [TestClass]
    public class ActionTests
    {
        [TestMethod]
        public void ControllerTest()
        {
            // Arrange - Create the controller
            ExampleController target = new ExampleController();

            // Act - Call the action method
            ViewResult result = target.Index();

            // Assert - Check the result
            Assert.AreEqual("Homepage", result.ViewName);
        }

        [TestMethod]
        public void ViewSelection()
        {
            // Arrange - Create the controller
            ExampleController target = new ExampleController();

            // Act - Call the action method
            ViewResult result = target.IndexWithViewName();

            // Assert - Check the result
            Assert.AreEqual("", result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(System.DateTime));
        }

        [TestMethod]
        public void ControllerViewBag()
        {
            // Arrange - Create the controller
            ExampleController target = new ExampleController();

            // Act - Call the action method
            ViewResult result = target.IndexWithViewBag();

            // Assert - Check the result
            Assert.AreEqual("Hello", result.ViewBag.Message);
        }

        [TestMethod]
        public void LiteralRederictions()
        {
            // Arrange - Create the controller
            ExampleController target = new ExampleController();

            // Act - Call the action method
            RedirectResult result = target.RedirectUserPermanently();

            // Assert - Check the result
            Assert.IsTrue(result.Permanent);
            Assert.AreEqual("/Example/Index", result.Url);
        }

        [TestMethod]
        public void RoutedRedirections()
        {
            // Arrange - Create the controller
            ExampleController target = new ExampleController();

            // Act - Call the action method
            RedirectToRouteResult result = target.RoutedRedirections();

            // Assert - Check the result
            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("Example", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("MyId", result.RouteValues["Id"]);
        }

        [TestMethod]
        public void HttpStatusCodes()
        {
            // Arrange - Create the controller
            ExampleController target = new ExampleController();

            // Act - Call the action method
            HttpStatusCodeResult result = target.StatusCode401();

            // Assert - Check the result
            Assert.AreEqual(401, result.StatusCode);
        }
    }
}
