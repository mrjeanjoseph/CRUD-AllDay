using System.Web.Mvc;

namespace IntroductionToMVC.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ActionResult Index() {
            return View();
        }
    }
}