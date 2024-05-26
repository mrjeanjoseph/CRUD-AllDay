using Chapter19.ControllerExtensibility.Infrastructure;
using System.Web.Mvc;

namespace Chapter19.ControllerExtensibility.Controllers
{
    public class ActionInvokerController : Controller
    {
        public ActionInvokerController() => this.ActionInvoker = new CustomActionInvoker();
        
    }
}