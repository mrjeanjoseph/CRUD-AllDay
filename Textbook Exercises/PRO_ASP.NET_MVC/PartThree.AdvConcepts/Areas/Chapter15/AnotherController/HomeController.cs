using System.Web.Mvc;

namespace Chapter15.URLsAndRoutes.AnotherControllers
{
    public class HomeController : Controller
    {
        // GET: Chapter15/Home
        public ActionResult Index()
        {
            ViewBag.Controller = "Another Controllers = Home";
            ViewBag.Action = "Index";

            return View("ActionName");
        }
    }
}