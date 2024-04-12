using System.Web.Mvc;
using WorkingWithRazor.Models;

namespace WorkingWithRazor.Controllers {
    public class HomeController : Controller {
        readonly Product createProduct = new Product {
            ProductID = 101,
            Name = "Bouret",
            Description = "Yon gros bouret",
            Category = "Biznis Pesonel",
            Price = 5725M,
            IsAvailable = true
        };

        readonly Product[] productArray = {
            new Product {Name = "Kamyon Dlo", Price = 97M},
            new Product {Name = "Ven Sak Sik", Price = 58M},
            new Product {Name = "Senkant Kes Kola", Price = 101M},
            new Product {Name = "Twa San Sak Mayi", Price = 179M},
            new Product {Name = "24 Rejim Bannann", Price = 85M}
        };

        // GET: Chapter5/Home
        public ActionResult Index() {
            return View(createProduct);
        }

        public ActionResult NameAndPrice() {
            return View(createProduct);
        }

        public ActionResult DemoExpression() {
            ViewBag.ProductCount = 1;
            ViewBag.ExpressShip = true;
            ViewBag.ApplyDiscount = false;
            ViewBag.Supplier = null;

            return View(createProduct);
        }

        public ActionResult DemoArray() {
            ViewBag.Message = "No Product data.";
            return View(productArray);
        }
    }
}