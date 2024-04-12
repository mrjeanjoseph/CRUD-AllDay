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
    }
}