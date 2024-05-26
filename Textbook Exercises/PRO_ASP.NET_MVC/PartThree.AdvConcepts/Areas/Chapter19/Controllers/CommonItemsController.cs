using Chapter19.ControllerExtensibility.Models;
using System.Web.Mvc;

namespace Chapter19.ControllerExtensibility.Controllers
{
    public class CommonItemsController : Controller
    {
        public ViewResult Index()
        {
            return View("Result", new Result
            {
                ResultId = 0,
                ControllerName = "CommonItems",
                ActionName = "Index"
            });
        }
        public ViewResult ListAllItems()
        {
            return View("Result", new Result
            {
                ResultId = 0,
                ControllerName = "CommonItems",
                ActionName = "ListAllItems"
            });
        }
    }
}