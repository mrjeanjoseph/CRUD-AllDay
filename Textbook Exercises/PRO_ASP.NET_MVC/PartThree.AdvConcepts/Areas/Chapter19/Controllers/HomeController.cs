using Chapter19.ControllerExtensibility.Infrastructure;
using Chapter19.ControllerExtensibility.Models;
using System.Web.Mvc;

namespace Chapter19.ControllerExtensibility.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View("Result", new Result
            {
                ResultId = 0,
                ControllerName = "Home",
                ActionName = "Index"
            });
        }

        [ActionName("Index")]
        public ViewResult ListAll()
        {
            return View("Result", new Result
            {
                ResultId = 0,
                ControllerName = "Home",
                ActionName = "ListAllItems"
            });
        }

        [Local]
        [ActionName("Index")]
        public ViewResult LocalActionMethod()
        {
            return View("Result", new Result
            {
                ResultId = 0,
                ControllerName = "Home",
                ActionName = "LocalActionMethod"
            });
        }

        protected override void HandleUnknownAction(string actionName)
        {
            Response.Write(string.Format("You requested the {0} action", actionName));
        }
    }
}