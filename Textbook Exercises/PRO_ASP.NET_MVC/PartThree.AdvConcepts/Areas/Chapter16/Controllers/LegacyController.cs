using System.Web.Mvc;

namespace Chapter16.URLsAndRoutes.Controllers
{
    public class LegacyController : Controller
    {
        // GET: Chapter16/Legacy
        public ActionResult GetLegacyURL(string legacyUrl)
        {
            return View((object)legacyUrl);
        }
    }
}