using System.Web.Mvc;

namespace Chapter18.ApplyingFilters.Controllers
{
    public class AdminController : Controller
    {
        [CustomAuth(false)]
        // GET: Chapter18/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}