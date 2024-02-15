using ControllerExtensibility.Models;
using System.Web.Mvc;

namespace ControllerExtensibility.Controllers {
    public class CustomerController : Controller {
        public ViewResult Index() {
            return View("Result", new Result { ControllerName = "Customer", ActionName = "Index" });
        }

        [ActionName("User-Registration")]
        public ViewResult List() {
            return View("Result", new Result { ControllerName = "Customer", ActionName = "List" });
        }

        [NonAction] public ActionResult FaveAction() {
            return View();
        }
    }
}