using ControllerExtensibility.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ControllerExtensibility.Controllers {
    public class RemoteDataController : Controller {
        public ActionResult DataOld() {
            RemoteService service = new RemoteService();
            string data = service.GetRemoteData();
            return View((object)data);
        }

        public async Task<ActionResult> Data() {
            string data = await Task<string>.Factory.StartNew(() => {
                return new RemoteService().GetRemoteData();
            });

            return View((object)data);
        }

        public async Task<ActionResult> ConsumeAsyncMethod() {
            string data = await new RemoteService().GetRemoteDataAsync();
            return View("Data", (object)data);
        }
    }
}