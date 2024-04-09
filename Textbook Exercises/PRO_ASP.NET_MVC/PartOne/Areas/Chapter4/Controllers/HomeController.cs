using EssentialFeatures.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EssentialFeatures.Controllers {
    public class HomeController : Controller {
        readonly string HomePageHeader = "This is the Essential Features Home page";

        public string Index() {

            string indexpageresult = "Navigate to a URL to view examples";
            return $"<h2>{HomePageHeader}</h2>\n{indexpageresult}";
        }



        public ViewResult UseExtention() {
            //Create and Population ShoppingCart
            ShoppingCart cart = new ShoppingCart {
                Products = new List<Product> {
                    new Product { ProductName = "Mayi Moulen", ProductPrice = 250 },
                    new Product { ProductName = "Kess Malta", ProductPrice = 115 },
                    new Product { ProductName = "Diri Blanc", ProductPrice = 175 },
                    new Product { ProductName = "Fey Lalo", ProductPrice = 350 },
                    new Product { ProductName = "Vyann Zandwi", ProductPrice = 75 },
                }
            };

            //get the total value of the products in the cart
            decimal cartTotal = cart.TotalPrices();
            return View("Index", (object)String.Format("Total: {0:c}", cartTotal));
        }

        public ViewResult CreateCollection() {
            string[] stringArray = { "Kabrit", "Poul", "Kanna", "Mayi Moulen", "Mango Fransik" };

            List<int> listofnums = new List<int> { 11, 27, 31, 09, 01 };

            Dictionary<string, int> keyValuePairs = new Dictionary<string, int> {
                { "Cheval", 501 },
                { "Bouk Kabrit", 488 },
                { "Pye Mango", 313 },
                { "Kombit", 198 },
                { "Regime Bannan", 105 }
            };

            return View("Index", (object)stringArray[1]);
        }

        public ViewResult CreateProduct() {
            //Using object initializer feature
            //Create and populate a new product object
            Product productInit = new Product {
                ProductID = 101,
                ProductName = "Yon Gwo Milet",
                Description = "Yon milet ki gwoneg and ki gen kouraj",
                ProductPrice = 190,
                Category = "Jardin"
            };

            return View("Index", (object)String.Format("{0} Category: {1}", HomePageHeader, productInit.Description));

        }

        public ViewResult CreateProductOld() {
            //Create a new Product object
            Product myProduct = new Product();

            //set the property values
            myProduct.ProductID = 101;
            myProduct.ProductName = "Pye Kokoye";
            myProduct.Description = "Pye kokoye bo yon rivye";
            myProduct.ProductPrice = 190;
            myProduct.Category = "Jardin";

            return View("Index", (object)String.Format("{0} Category: {1}", HomePageHeader, myProduct.ProductName));
        }

        public ViewResult AutoProperty() {
            //Create a new Product object
            DefiningProperty_OldWays myProduct = new DefiningProperty_OldWays();

            //Set the property
            myProduct.Name = "Riview Laskawob";

            //Get the property
            string productName = myProduct.Name;

            //Generate the view
            return View("Index", (object)String.Format("Product Name: {0}", productName));
        }
    }
}