using System.Web.Mvc;

namespace Chapter15.URLsAndRoutes.Controllers
{
    public class AdminController : Controller
    {
        // GET: Chapter15/Admin
        public ActionResult Index()
        {
            ViewBag.Controller = "Admin";
            ViewBag.Action = "Index";

            return View("ActionName");
        }
    }
}