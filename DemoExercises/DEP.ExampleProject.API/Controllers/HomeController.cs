using System.Web.Mvc;

namespace DEP.ExampleProject.API.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
