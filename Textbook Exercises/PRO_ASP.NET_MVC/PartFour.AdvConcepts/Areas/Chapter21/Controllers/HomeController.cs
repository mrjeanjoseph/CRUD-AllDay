using System.Web.Mvc;

namespace Chapter21.HelperMethods.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter21/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}