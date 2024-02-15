using ControllerExtensibility.Models;
using System.Web.Mvc;

namespace ControllerExtensibility.Controllers {
    public class RemoteDataController : Controller {
        public ActionResult Data() {
            RemoteService service = new RemoteService();
            string data = service.GetRemoteData();
            return View((object)data);
        }
    }
}