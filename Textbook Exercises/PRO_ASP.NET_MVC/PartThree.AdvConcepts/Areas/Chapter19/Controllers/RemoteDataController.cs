using Chapter19.ControllerExtensibility.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Chapter19.ControllerExtensibility.Controllers
{
    public class RemoteDataController : Controller
    {
        public async Task<ActionResult> Data()
        {
            string data = await Task<string>.Factory.StartNew(() =>
            {
                return new RemoteService().GetRemoteData();
            });
            return View((object)data);
        }

        public async Task<ActionResult> ConsumeDataAsync()
        {
            string data = await new RemoteService().GetRemoteDataSync();
            return View((object)data);
        }

        public ActionResult DataOld()
        {
            RemoteService service = new RemoteService();
            string data = service.GetRemoteData();
            return View((object)data);
        }
    }
}