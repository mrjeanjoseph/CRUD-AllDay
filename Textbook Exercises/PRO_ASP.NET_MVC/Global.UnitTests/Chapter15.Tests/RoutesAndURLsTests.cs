using AdvConcepts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace ChapterFifteen.Tests
{
    [TestClass]
    public class RoutesAndURLsTests
    {
        private HttpContextBase CreateHttpContext(
            string targetUrl = null, string httpMethod = "GET")
        {
            // create the mock request
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            // create the mock response
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            // create the mock context, using the request and response
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            // return the mock context
            return mockContext.Object;
        }

        private void TestRouteMatch(string url, string controller,
            string action, object routeProperties = null, string httpMethod = "GET")
        {
            // Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act - Process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
        }

        private bool TestIncomingRouteResult(
            RouteData routeResult, string controller,
            string action, object propertySet = null)
        {
            Func<object, object, bool> valCompare = (v1, v2) =>
            {
                return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
            };

            bool result = valCompare(routeResult.Values["controller"], controller)
                && valCompare(routeResult.Values["action"], action);

            if (propertySet != null)
            {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach (PropertyInfo info in propInfo)
                {
                    if (!(routeResult.Values.ContainsKey(info.Name)
                        && valCompare(routeResult.Values[info.Name], info.GetValue(propertySet, null))))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        private void TestRouteFail(string url)
        {
            // Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act - process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url));

            // Assert
            Assert.IsTrue(result == null || result.Route == null);
        }

        [TestMethod]
        public void TestIncomingRoutes()
        {
            // check for the url that is hoped for
            TestRouteMatch("~/URLsAndRoutes/Admin/Index", "Admin", "Index");

            // check that the values are being obtained from the segments
            TestRouteMatch("~/One/Two", "One", "Two");

            // ensure that too many or too few segments fails to match
            TestRouteFail("/URLsAndRoutes/Admin/Index/Segment");
            TestRouteFail("/URLsAndRoutes/Admin");
        }

        [TestMethod]
        public void TestingStaticSegments()
        {

            TestRouteMatch("~URLsAndRoutes", "Home", "Index");
            TestRouteMatch("~URLsAndRoutes/Customer", "Customer", "Index");
            TestRouteMatch("~URLsAndRoutes/Customer/List", "Customer", "List");
            TestRouteFail("~URLsAndRoutes/Customer/List/All");
            TestRouteMatch("~URLsAndRoutes/Shop/Index", "Home", "Index");
        }

        [TestMethod]
        public void CustomSegmentVariables()
        {

            TestRouteMatch("~/URLsAndRoutes", "Home", "Index", new {id = "DefaultId"});
            TestRouteMatch("~/URLsAndRoutes/Customer", "Customer", "Index", 
                new { id = "DefaultId" });
            TestRouteMatch("~/URLsAndRoutes/Customer/List", "Customer", "List", 
                new { id = "DefaultId" });
            TestRouteMatch("~/URLsAndRoutes/Customer/List/All", "Customer", "List", 
                new { id = "DefaultId" });
            TestRouteFail("~/URLsAndRoutes/Customer/List/All/Delete");
        }

        [TestMethod]
        public void OptionalUrlSegments()
        {
            TestRouteMatch("~/URLsAndRoutes", "Home", "Index");
            TestRouteMatch("~/URLsAndRoutes/Customer", "Customer", "Index");
            TestRouteMatch("~/URLsAndRoutes/Customer/List", "Customer", "List");
            TestRouteMatch("~/URLsAndRoutes/Customer/List/All", "Customer", "List",
                new { id = "All"});
            TestRouteFail("~/URLsAndRoutes/Customer/List/All/Delete");
        }

        [TestMethod]
        public void CatchallSegmentVariables()
        {
            TestRouteMatch("~/URLsAndRoutes", "Home", "Index");
            TestRouteMatch("~/URLsAndRoutes/Customer", "Customer", "Index");
            TestRouteMatch("~/URLsAndRoutes/Customer/List", "Customer", "List");
            TestRouteMatch("~/URLsAndRoutes/Customer/List/All", "Customer", "List",
                new { id = "All"});
            TestRouteMatch("~/URLsAndRoutes/Customer/List/All/Delete", "Customer", "List",
                new { id = "All", catchall = "Delete"});
            TestRouteMatch("~/URLsAndRoutes/Customer/List/All/Delete/Perm", "Customer", "List",
                new { id = "All", catchall = "Delete/Perm"});
        }

        [TestMethod]
        public void TestingRouteConstraints()
        {
            TestRouteMatch("~/URLsAndRoutes", "Home", "Index");
            TestRouteMatch("~/URLsAndRoutes/Home", "Home", "Index");
            TestRouteMatch("~/URLsAndRoutes/Home/Index", "Home", "Index");

            TestRouteMatch("~/URLsAndRoutes/About", "Home", "About");
            TestRouteMatch("~/URLsAndRoutes/About/MyId", "Home", "About", 
                new {id = "MyId"});
            TestRouteMatch("~/URLsAndRoutes/About/MyId/More/Segments", "Home", "About", 
                new {id = "MyId", catchall = "More/Segments" });

            TestRouteFail("~/Home/OtherAction");
            TestRouteFail("~/Account/Index");
            TestRouteFail("~/Account/About");

        }
    }
}
