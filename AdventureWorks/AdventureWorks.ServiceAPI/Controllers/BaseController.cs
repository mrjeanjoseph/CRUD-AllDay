using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.ServiceAPI.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
