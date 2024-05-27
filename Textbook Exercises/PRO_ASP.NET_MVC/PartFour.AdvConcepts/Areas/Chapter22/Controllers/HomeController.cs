using System.Web.Mvc;

namespace Chapter22.TemplatedHelperMethods.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
    }
}