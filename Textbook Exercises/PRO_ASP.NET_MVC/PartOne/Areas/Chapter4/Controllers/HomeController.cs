using EssentialFeatures.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace EssentialFeatures.Controllers {
    public class HomeController : Controller {
        readonly string HomePageHeader = "This is the Essential Features Home page";

        public string Index() {

            string indexpageresult = "Navigate to a URL to view examples";
            return $"<h2>{HomePageHeader}</h2>\n{indexpageresult}";
        }

        #region Other ViewResult

        public ViewResult SumAllProducts() {
            Product[] productArray = {
                new Product { ProductName = "Mayi Moulen", Category = "Jadin", ProductPrice = 250 },
                new Product { ProductName = "Kess Malta", Category = "Boutik", ProductPrice = 115 },
                new Product { ProductName = "Diri Blanc", Category = "Jadin", ProductPrice = 175 },
                new Product { ProductName = "Fey Lalo", Category = "Jadin", ProductPrice = 350 },
                new Product { ProductName = "Vyann Zandwi", Category = "Vann Manje", ProductPrice = 75 }
            };

            var result = productArray.Sum(p => p.ProductPrice);

            //Notice the total will be deferred
            productArray[2] = new Product { ProductName = "Pye Palmis", ProductPrice = 9000 };

            return View("Index", (object)result.ToString());
        }        

        public ViewResult FindProductsWithDotNotation() {
            Product[] productArray = {
                new Product { ProductName = "Mayi Moulen", Category = "Jadin", ProductPrice = 250 },
                new Product { ProductName = "Kess Malta", Category = "Boutik", ProductPrice = 115 },
                new Product { ProductName = "Diri Blanc", Category = "Jadin", ProductPrice = 175 },
                new Product { ProductName = "Fey Lalo", Category = "Jadin", ProductPrice = 350 },
                new Product { ProductName = "Vyann Zandwi", Category = "Vann Manje", ProductPrice = 75 }
            };

            //DOT Notation is another option
            var foundProducts = productArray
                .OrderByDescending(e => e.ProductPrice)
                .Take(3).Select(p => new { p.ProductName, p.ProductPrice });

            //Here we can use Deferred LINQ Queries
            productArray[2] = new Product { ProductName = "Pye Palmis", ProductPrice = 9000 };

            //Create the result
            StringBuilder result = new StringBuilder();
            foreach (var product in foundProducts) {
                result.AppendFormat("Price: {0}, ", product.ProductPrice);
            }

            return View("Index", (object)result.ToString());
        }

        public ViewResult FindProductsWithLINQ() {
            Product[] productArray = {
                new Product { ProductName = "Mayi Moulen", Category = "Jadin", ProductPrice = 250 },
                new Product { ProductName = "Kess Malta", Category = "Boutik", ProductPrice = 115 },
                new Product { ProductName = "Diri Blanc", Category = "Jadin", ProductPrice = 175 },
                new Product { ProductName = "Fey Lalo", Category = "Jadin", ProductPrice = 350 },
                new Product { ProductName = "Vyann Zandwi", Category = "Vann Manje", ProductPrice = 75 }
            };

            //LINQ's SQL-like query
            var foundProducts = from match in productArray
                                orderby match.ProductPrice descending
                                select new {match.ProductName, match.ProductPrice};

            //Create the result
            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var product in foundProducts) {
                result.AppendFormat("Price: {0}\n ", product.ProductPrice);
                if (++count == 3) break;                
            }

            return View("Index", (object)result.ToString());
        }

        public ViewResult FindProducts() {
            Product[] productArray = {
                new Product { ProductName = "Mayi Moulen", Category = "Jadin", ProductPrice = 250 },
                new Product { ProductName = "Kess Malta", Category = "Boutik", ProductPrice = 115 },
                new Product { ProductName = "Diri Blanc", Category = "Jadin", ProductPrice = 175 },
                new Product { ProductName = "Fey Lalo", Category = "Jadin", ProductPrice = 350 },
                new Product { ProductName = "Vyann Zandwi", Category = "Vann Manje", ProductPrice = 75 }
            };

            //define the array to hold the results
            Product[] foundProducts = new Product[3];

            //sort the content of the array
            Array.Sort(productArray, (a, b) => {
                return Comparer<decimal>.Default.Compare(a.ProductPrice, b.ProductPrice);
            });

            //get the first three items in the array as the results
            Array.Copy(productArray, foundProducts, 3);

            //Create the result
            StringBuilder result = new StringBuilder();
            foreach (Product product in foundProducts) {
                result.AppendFormat("Price: {0}\n ", product.ProductPrice);
            }

            return View("Index", (object)result.ToString());
        }

        public ViewResult CreateAnonymousArray() {
            var oddsAndEnds = new[] {
                new {Name = "Zoranj Si", Category = "Fwi"},
                new {Name = "Tomat dous", Category = "Vegetab"},
                new {Name = "Zaboka", Category = "Vegetab"},
                new {Name = "Mango Fransik", Category = "Fwi"},
                new {Name = "Patat Si", Category = "Vegetab"}
            };

            StringBuilder result = new StringBuilder();
            foreach (var item in oddsAndEnds) {
                result.Append(item.Name).Append(", ");
            }

            return View("Index", (object)result.ToString());
        }

        public JsonResult AnonymousType() {
            var AnonymousUser = new {
                FirstName = "Louque",
                LastName = "Jean-Jacques",
                //DateOfBirth = new DateTime(7, 15, 1981).ToShortDateString(),
                PhysicalAddress = new {
                    StreetNumber = 2552,
                    StreeName = "Money Tree Lan",
                    City = "Raleigh-Durham",
                    State = "North Carolina",
                    ZipCode = "27610-6615"
                },
                PhoneNumber = "9195009900"
            };

            return Json(AnonymousUser, JsonRequestBehavior.AllowGet);
        }

        public ViewResult UseFilterOverAll() {
            //Create and Population an list of product object
            IEnumerable<Product> productEnum = new ShoppingCart {
                Products = new List<Product> {
                    new Product { ProductName = "Mayi Moulen", Category = "Jadin", ProductPrice = 250 },
                    new Product { ProductName = "Kess Malta", Category = "Boutik", ProductPrice = 115 },
                    new Product { ProductName = "Diri Blanc", Category = "Jadin", ProductPrice = 175 },
                    new Product { ProductName = "Fey Lalo", Category = "Jadin", ProductPrice = 350 },
                    new Product { ProductName = "Vyann Zandwi", Category = "Vann Manje", ProductPrice = 75 },
                }
            };

            Func<Product, bool> categoryFilter = delegate (Product prod) {
                return prod.Category == "Boutik";
            };

            //We can also use a one liner
            Func<Product, bool> categoryFilterOneLiner = prod => prod.Category == "Vann Manje";

            decimal total = 0;
            foreach (Product product in productEnum.FilterOverAll(categoryFilterOneLiner)) {
                total += product.ProductPrice;
            }
            decimal total2 = 0;
            foreach (Product product in productEnum.FilterOverAll(prod => prod.Category == "Boutik")) {
                total2 += product.ProductPrice;
            }
            decimal total3 = 0;
            foreach (Product product in productEnum.FilterOverAll(
                prod => prod.Category == "Boutik" || prod.ProductPrice > 100)) {
                total3 += product.ProductPrice;
            }

            return View("Index", (object)String.Format("Total: {0:c}", total3));
        }

        public ViewResult UseFilterExtentionMethod(string category) {
            //Create and Population an list of product object
            IEnumerable<Product> productEnum = new ShoppingCart {
                Products = new List<Product> {
                    new Product { ProductName = "Mayi Moulen", Category = "Jadin", ProductPrice = 250 },
                    new Product { ProductName = "Kess Malta", Category = "Boutik", ProductPrice = 115 },
                    new Product { ProductName = "Diri Blanc", Category = "Jadin", ProductPrice = 175 },
                    new Product { ProductName = "Fey Lalo", Category = "Jadin", ProductPrice = 350 },
                    new Product { ProductName = "Vyann Zandwi", Category = "Vann Manje", ProductPrice = 75 },
                }
            };

            category = "Jadin";

            decimal total = 0;
            foreach (Product product in productEnum.FilterByCategory(category)) {
                total += product.ProductPrice;
            }

            return View("Index", (object)String.Format("Total {0}: {1:c}", category, total));
        }

        public ViewResult UseExtentionEnumerable() {
            //Create and Population an list of product object
            IEnumerable<Product> productEnum = new ShoppingCart {
                Products = new List<Product> {
                    new Product { ProductName = "Mayi Moulen", ProductPrice = 250 },
                    new Product { ProductName = "Kess Malta", ProductPrice = 115 },
                    new Product { ProductName = "Diri Blanc", ProductPrice = 175 },
                    new Product { ProductName = "Fey Lalo", ProductPrice = 350 },
                    new Product { ProductName = "Vyann Zandwi", ProductPrice = 75 },
                }
            };

            //Create and populate an array of Product objects
            Product[] productArray = {
                new Product { ProductName = "Mayi Moulen", ProductPrice = 250 },
                new Product { ProductName = "Kess Malta", ProductPrice = 115 },
                new Product { ProductName = "Diri Blanc", ProductPrice = 175 },
                new Product { ProductName = "Fey Lalo", ProductPrice = 350 },
                new Product { ProductName = "Vyann Zandwi", ProductPrice = 75 },
            };

            //get the total value of the products in the cart
            decimal listTotal = productEnum.TotalPrices();
            decimal arrayTotal = productArray.TotalPrices();
            return View("Index", (object)String.Format("List Total: {0:c}, Array Total:{0:c}", listTotal, arrayTotal));
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

        #endregion
    }
}