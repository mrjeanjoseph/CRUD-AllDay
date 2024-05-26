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
        public ViewResult ListAllItems()
        {
            return View("Result", new Result
            {
                ResultId = 0,
                ControllerName = "PremiumItems",
                ActionName = "ListAllItems"
            });
        }
    }
}