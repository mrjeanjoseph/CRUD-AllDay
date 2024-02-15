using ControllerExtensibility.Infrastructure;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;

namespace ControllerExtensibility.Controllers {
    public class HomeController : Controller {
        // GET: Home
        //public ActionResult Index() {
        //    return View("Result", new Result {
        //        ControllerName = "Home",
        //        ActionName = "Index"
        //    });
        //}

        //[Local, ActionName("Index")]
        //public ActionResult LocalIndex() {
        //    return View("Result", new Result {
        //        ControllerName = "Home",
        //        ActionName = "Index"
        //    });
        //}

        protected override void HandleUnknownAction(string actionName) {
            Response.Write(string.Format("You requested the {0} action", actionName));
        }
    }
}