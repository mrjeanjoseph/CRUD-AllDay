using System.Web.Mvc;
using System.Web.SessionState;

namespace CreatingStatefulData.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    public class SyncTestController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public string GetMessage(int id)
        {
            return string.Format("Message: {0}", id);
        }
    }
}