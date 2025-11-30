using Microsoft.AspNetCore.Mvc;

namespace TimesheetManagement.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error()
        {
            HttpContext.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            HttpContext.Response.Headers["Pragma"] = "no-cache";
            HttpContext.Response.Headers["Expires"] = "0";
            HttpContext.Session.Clear();
            return View("Error");
        }
    }
}