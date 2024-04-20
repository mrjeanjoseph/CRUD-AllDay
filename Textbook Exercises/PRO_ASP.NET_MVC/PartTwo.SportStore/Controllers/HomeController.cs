using System.Web.Mvc;

namespace SportStore.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ActionResult Index() {
            return View();
        }
    }
}