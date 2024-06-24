using CreatingStatefulData.Infrastructure;
using System.Web.Mvc;

namespace CreatingStatefulData.Controllers
{
    public class LifecycleController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(bool store = false, bool abandon = false)
        {
            if (store)            
                SessionStateHelper.Set(SessionStateKeys.NAME, "Kervens");

            if (abandon) Session.Abandon();

            return RedirectToAction("Index");
        }
    }
}