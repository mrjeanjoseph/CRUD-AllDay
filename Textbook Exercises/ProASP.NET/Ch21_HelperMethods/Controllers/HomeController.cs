using Ch21_HelperMethods.Models;
using System.Web.Mvc;

namespace Ch21_HelperMethods.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ActionResult Index() {

            ViewBag.Fruits = new string[] { "Gwayav", "Melon", "Pistash", "Mango Fransik" };
            ViewBag.Cities = new string[] { "Lonsie", "Rwasek", "Plateau Central", "Delmar 24" };

            string message = "This is an HTML element: <input>";
            return View((object)message);
        }

        public ActionResult CreatePerson() {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult CreatePerson(Person person) {
            return View("DisplayPerson", person);
        }
    }
}