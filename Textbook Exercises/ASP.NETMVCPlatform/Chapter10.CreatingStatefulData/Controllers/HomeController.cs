using System.Collections.Generic;
using System.Web.Mvc;

namespace CreatingStatefulData.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Dictionary<string, object> dataOject = new Dictionary<string, object>();
            dataOject.Add("Placeholder Property", "Placeholder Value");
            return View(dataOject);
        }
    }
}