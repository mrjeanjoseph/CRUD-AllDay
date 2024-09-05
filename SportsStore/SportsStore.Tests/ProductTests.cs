using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using SportsStore.Domain.Abstract;
using System.Linq;

namespace SportsStore.Tests {
    [TestClass]
    public class ProductTests {

        [TestMethod]
        public void CanPaginate() {

            //mock product repository
            Mock<IProductRepository> _mock = new Mock<IProductRepository>();
            _mock.Setup(m => m.Products).Returns(
                new List<Product> {
                    new Product(){ProductID=1,Name="Product1"},
                    new Product(){ProductID=1,Name="Product2"},
                    new Product(){ProductID=1,Name="Product3"},
                    new Product(){ProductID=1,Name="Product4"},
                    new Product(){ProductID=1,Name="Product5"},
                    new Product(){ProductID=1,Name="Product6"},
                    new Product(){ProductID=1,Name="Product7"},
                    new Product(){ProductID=1,Name="Product8"},
                    new Product(){ProductID=1,Name="Product9"},
                    new Product(){ProductID=1,Name="Product10"},
                    new Product(){ProductID=1,Name="Product11"},
                    new Product(){ProductID=1,Name="Product12"}
                }.AsQueryable()
            );

            //target class
            ProductController target = new ProductController(_mock.Object);
            target.pageSize = 2;
            ProductsListViewModel actual;
            actual = (ProductsListViewModel)target.List(null, 1).Model;
            Product[] actualresult;
            actualresult = actual.Products.ToArray();

            //Assert
            Assert.AreEqual(2, actualresult.Length);
            Assert.AreEqual("Product1", actualresult[0].Name);
            Assert.AreEqual("Product2", actualresult[1].Name);

            actual = (ProductsListViewModel)target.List(null, 2).Model;
            actualresult = actual.Products.ToArray();

            //Assert
            Assert.AreEqual(2, actualresult.Length);
            Assert.AreEqual("Product3", actualresult[0].Name);
            Assert.AreEqual("Product4", actualresult[1].Name);
        }

        [TestMethod]
        public void CanAddPageLink() {
            PagingInfo vpagingInfo = new PagingInfo() {
                TotalItems = 11,
                ItemsPerPage = 3,
                CurrentPage = 2
            };

            Func<int, string> vpageUrl = m => "Page:" + m;
            MvcHtmlString resulthtml = PagingHelpers.PageLinks(null, vpagingInfo, vpageUrl);
            string target = resulthtml.ToString();

            //<a href=""Page:1"" class=""selected"" />
            string expected = @"<a href=""Page:1"">1</a>" +
                            @"<a class=""selected"" href=""Page:2"">2</a>" +
                            @"<a href=""Page:3"">3</a>";
            Assert.AreEqual(expected, target);
        }

        [TestMethod]
        public void SendPaginationViewModel() {

            //mock product repository
            Mock<IProductRepository> _mock = new Mock<IProductRepository>();
            _mock.Setup(m => m.Products).Returns(
                new List<Product> {
                    new Product(){ProductID=1,Name="Product1"},
                    new Product(){ProductID=1,Name="Product2"},
                    new Product(){ProductID=1,Name="Product3"},
                    new Product(){ProductID=1,Name="Product4"},
                    new Product(){ProductID=1,Name="Product5"},
                    new Product(){ProductID=1,Name="Product6"},
                    new Product(){ProductID=1,Name="Product7"},
                    new Product(){ProductID=1,Name="Product8"},
                    new Product(){ProductID=1,Name="Product9"},
                    new Product(){ProductID=1,Name="Product10"},
                    new Product(){ProductID=1,Name="Product11"},
                    new Product(){ProductID=1,Name="Product12"}
                }.AsQueryable()
            );

            //target class
            ProductController target = new ProductController(_mock.Object);
            target.pageSize = 4;

            ProductsListViewModel _m = (ProductsListViewModel)target.List(null, 2).Model;

            Assert.AreEqual(2, _m.PagingInfo.CurrentPage);
            Assert.AreEqual(12, _m.PagingInfo.TotalItems);
            Assert.AreEqual(3, _m.PagingInfo.TotalPages);
        }

        [TestMethod]
        public void CanFilterProducts() {

            //setup mock product repository
            Mock<IProductRepository> _mock = new Mock<IProductRepository>();
            _mock.Setup(m => m.Products).Returns(
                new List<Product> {
                    new Product(){ProductID=1,Name="Product1",Category="c1"},
                    new Product(){ProductID=1,Name="Product2",Category="c1"},
                    new Product(){ProductID=1,Name="Product3",Category="c1"},
                    new Product(){ProductID=1,Name="Product4",Category="c1"},
                    new Product(){ProductID=1,Name="Product5",Category="c2"},
                    new Product(){ProductID=1,Name="Product6",Category="c2"},
                    new Product(){ProductID=1,Name="Product7",Category="c2"},
                    new Product(){ProductID=1,Name="Product8",Category="c2"},
                    new Product(){ProductID=1,Name="Product9",Category="c2"},
                    new Product(){ProductID=1,Name="Product10",Category="c2"},
                    new Product(){ProductID=1,Name="Product11",Category="c3"},
                    new Product(){ProductID=1,Name="Product12",Category="c3"}
                }.AsQueryable()
            );

            //target
            ProductController target = new ProductController(_mock.Object);
            ProductsListViewModel _m = (ProductsListViewModel)target.List("c3").Model;
            Assert.AreEqual(2, _m.Products.Count());
        }

        [TestMethod]
        public void CanCreateCategories() {

            //mock product repository
            Mock<IProductRepository> _mock = new Mock<IProductRepository>();

            _mock.Setup(m => m.Products).Returns(
                new List<Product> {
                    new Product(){ProductID=1,Name="Product1",Category="c8"},
                    new Product(){ProductID=1,Name="Product2",Category="c8"},
                    new Product(){ProductID=1,Name="Product3",Category="c8"},
                    new Product(){ProductID=1,Name="Product4",Category="c8"},
                    new Product(){ProductID=1,Name="Product5",Category="c2"},
                    new Product(){ProductID=1,Name="Product6",Category="c2"},
                    new Product(){ProductID=1,Name="Product7",Category="c2"},
                    new Product(){ProductID=1,Name="Product8",Category="c2"},
                    new Product(){ProductID=1,Name="Product9",Category="c2"},
                    new Product(){ProductID=1,Name="Product10",Category="c2"},
                    new Product(){ProductID=1,Name="Product11",Category="c3"},
                    new Product(){ProductID=1,Name="Product12",Category="c3"}
                }.AsQueryable()
            );

            //verify that user can generate categories from NavController method
            NavController target = new NavController(_mock.Object);
            IEnumerable<string> actual = (IEnumerable<string>)target.Menu().Model;

            Assert.AreEqual(3, actual.Count());
            Assert.AreEqual("c2", actual.ElementAt(0));
            Assert.AreEqual("c3", actual.ElementAt(1));
            Assert.AreEqual("c8", actual.ElementAt(2));

        }

        [TestMethod]
        public void IndicatesSelectedCategory() {

            //mock product repository
            Mock<IProductRepository> _mock = new Mock<IProductRepository>();

            _mock.Setup(m => m.Products).Returns(
                new List<Product> {
                    new Product(){ProductID=1,Name="Product1",Category="c8"},
                    new Product(){ProductID=1,Name="Product2",Category="c8"},
                    new Product(){ProductID=1,Name="Product3",Category="c8"},
                    new Product(){ProductID=1,Name="Product4",Category="c8"},
                    new Product(){ProductID=1,Name="Product5",Category="c2"},
                    new Product(){ProductID=1,Name="Product6",Category="c2"},
                    new Product(){ProductID=1,Name="Product7",Category="c2"},
                    new Product(){ProductID=1,Name="Product8",Category="c2"},
                    new Product(){ProductID=1,Name="Product9",Category="c2"},
                    new Product(){ProductID=1,Name="Product10",Category="c2"},
                    new Product(){ProductID=1,Name="Product11",Category="c3"},
                    new Product(){ProductID=1,Name="Product12",Category="c3"}
                }.AsQueryable()
            );

            //verify that user can generate categories from NavController method
            NavController target = new NavController(_mock.Object);
            string actual = target.Menu("c2").ViewBag.selectedcategory;
            Assert.AreEqual("c2", actual);
        }

        [TestMethod]
        public void GenerateCategorySpecificProductCount() {

            //mock product repository
            Mock<IProductRepository> _mock = new Mock<IProductRepository>();

            _mock.Setup(m => m.Products).Returns(
                new List<Product> {
                    new Product(){ProductID=1,Name="Product1",Category="c8"},
                    new Product(){ProductID=1,Name="Product2",Category="c8"},
                    new Product(){ProductID=1,Name="Product3",Category="c8"},
                    new Product(){ProductID=1,Name="Product4",Category="c8"},
                    new Product(){ProductID=1,Name="Product5",Category="c2"},
                    new Product(){ProductID=1,Name="Product6",Category="c2"},
                    new Product(){ProductID=1,Name="Product7",Category="c2"},
                    new Product(){ProductID=1,Name="Product8",Category="c2"},
                    new Product(){ProductID=1,Name="Product9",Category="c2"},
                    new Product(){ProductID=1,Name="Product10",Category="c2"},
                    new Product(){ProductID=1,Name="Product11",Category="c3"},
                    new Product(){ProductID=1,Name="Product12",Category="c3"}
                }.AsQueryable()
            );

            //target class
            ProductController target = new ProductController(_mock.Object);
            target.pageSize = 10;

            ProductsListViewModel count_c3 = (ProductsListViewModel)target.List("c3", 1).Model;
            ProductsListViewModel count_c2 = (ProductsListViewModel)target.List("c2", 1).Model;
            ProductsListViewModel count_c8 = (ProductsListViewModel)target.List("c8", 1).Model;

            Assert.AreEqual(2, count_c3.Products.Count());
            Assert.AreEqual(6, count_c2.Products.Count());
            Assert.AreEqual(4, count_c8.Products.Count());
        }
    }
}
