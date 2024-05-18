using System.Web.Mvc;

namespace Chapter15.URLsAndRoutes.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Chapter15/Customer
        public ActionResult Index()
        {
            ViewBag.Controller = "Customer";
            ViewBag.Action = "Index";

            return View("ActionName");
        }
    }
}