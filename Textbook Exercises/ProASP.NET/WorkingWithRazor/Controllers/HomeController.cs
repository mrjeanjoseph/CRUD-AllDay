using System.Web.Mvc;

namespace WorkingWithRazor.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ActionResult Index() {

            string[] names = { "Apples", "Oranges", "Mango", "Guayava", "Passion Fruit" };
            return View(names);
        }
    }
}