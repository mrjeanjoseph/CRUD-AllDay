using Chapter19.ControllerExtensibility.Models;
using System.Web.Mvc;

namespace Chapter19.ControllerExtensibility.Controllers
{
    public class PremiumItemsController : Controller
    {
        public ViewResult Index()
        {
            return View("Result", new Result
            {
                ResultId = 0,
                ControllerName = "PremiumItems",
                ActionName = "Index"
            });
        }

        [ActionName("Enumerate")] //Obviously renaming the action
        public ViewResult ListAllItems() // Name no longer valid
        {
            return View("Result", new Result
            {
                ResultId = 0,
                ControllerName = "PremiumItems",
                ActionName = "ListAllItems"
            });
        }

        [NonAction]
        public ViewResult DoNotUseThisActionMethond()
        {
            //This will generate a 404 Not Found!
            return View();
        }
    }
}