using ControllerExtensibility.Models;
using System.Web.Mvc;
using System.Web.SessionState;

namespace ControllerExtensibility.Controllers {

    [SessionState(SessionStateBehavior.Disabled)]
    public class FastController : Controller {

        public ActionResult Index() {
            return View("Index", new Result { ControllerName = "Fast", ActionName = "Index" });
        }
    }
}