using System.Web.Mvc;

namespace Chapter15.URLsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter15/Home
        public ActionResult Index()
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "Index";

            return View("ActionName");
        }
    }
}