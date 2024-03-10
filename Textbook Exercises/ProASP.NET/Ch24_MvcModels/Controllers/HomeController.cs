using System.Web.Mvc;

namespace Ch24_MvcModels.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ActionResult Index() {
            return View();
        }
    }
}