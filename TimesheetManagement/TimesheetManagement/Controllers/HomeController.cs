using Microsoft.AspNetCore.Mvc;

namespace TimesheetManagement.Controllers
{
    public class HomeController : Controller
    {
        // Landing route hit at '/'
        public IActionResult Index()
        {
            // Redirect unauthenticated users to Login
            return RedirectToAction("Login", "Login");
        }
    }
}