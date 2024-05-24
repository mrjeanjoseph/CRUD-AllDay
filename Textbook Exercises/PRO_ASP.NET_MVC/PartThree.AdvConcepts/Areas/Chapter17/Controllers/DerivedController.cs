using System;
using System.Web.Mvc;

namespace Chapter17.ControllersAndActions.Controllers
{
    public class DerivedController : Controller
    {
        public ActionResult Index()
        {
            string machineName = Server.MachineName;
            ViewBag.Message = $"{machineName} - Hello from the Derived Controller Index method";
            return View("MyView");
        }

        public ActionResult ProduceOutputTwo()
        {
            if (Server.MachineName == "JeanPC")
                return new CustomRedirectResult { Url = "/Basic/Index" };
            else
            {
                Response.Write("Controller: Derived, Action: ProduceOutput");
                return null;
            }

        }

        public void ProduceOutput()
        {
            if (Server.MachineName == "JeanPC")
                Response.Redirect("/Basic/Index");
            else
                Response.Write("Controller: Derived, Action: ProduceOutput");

        }

        public ActionResult ShowWeatherForecast()
        {
            string city = (string)RouteData.Values["city"];
            DateTime forDate = DateTime.Parse(Request.Form["forDate"]);

            return null;

        }

        public ActionResult Hypothetically()
        {
            //Various properties from context objects
            string userName = User.Identity.Name;
            string serverName = Server.MachineName;
            string clientIP = Request.UserHostAddress;
            DateTime dateStamp = HttpContext.Timestamp;
            //AuditRequest(userName, serverName, clientIP, dateStamp, "Renaming Product");

            //Retrieve posted data from Request.Form
            string oldProductName = Request.Form["Old Name"];
            string NewProductName = Request.Form["New Name"];
            //bool result = AttemptProductRename(oldProductName, NewProductName);
            //ViewData["RenameResult"] = result;

            ViewBag.Message = $"{userName}, {serverName}, {clientIP}, {dateStamp}";

            return View("MyView");
        }

        private bool AttemptProductRename(string oldProductName, string newProductName)
        {
            throw new NotImplementedException();
        }

        private void AuditRequest(string userName,
            string serverName, string clientIP,
            DateTime dateStamp, string v)
        {

            throw new NotImplementedException();
        }
    }
}