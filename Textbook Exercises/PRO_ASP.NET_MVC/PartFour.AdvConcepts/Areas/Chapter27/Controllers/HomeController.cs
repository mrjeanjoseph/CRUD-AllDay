using System.Web.Mvc;

namespace Chapter27.WebServices.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter27/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}