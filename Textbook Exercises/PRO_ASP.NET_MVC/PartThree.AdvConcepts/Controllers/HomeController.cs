using System.Web.Mvc;

namespace PartThree.AdvConcepts.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}