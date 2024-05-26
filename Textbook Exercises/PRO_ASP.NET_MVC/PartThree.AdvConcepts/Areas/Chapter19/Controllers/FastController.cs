using Chapter19.ControllerExtensibility.Models;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Chapter19.ControllerExtensibility.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    public class FastController : Controller
    {
        public ViewResult Index()
        {
            return View("Result", new Result
            {
                ResultId = 0,
                ControllerName = "Fast",
                ActionName = "Index"
            });
        }
    }
}