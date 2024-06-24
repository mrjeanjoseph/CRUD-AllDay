using System.Diagnostics;
using System.Web.Mvc;

namespace CreatingStatefulData.Controllers
{
    public class RegistrationController : Controller 
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProcessFirstForm(string name)
        {
            Debug.WriteLine("Name: {0}", (object)name);
            return View("SecondForm");
        }

        [HttpPost]
        public ActionResult CompleteForm(string country)
        {
            Debug.WriteLine("Country: {0}", (object)country);
            //In a real application, this is where the call to create the new user
            ViewBag.Name = "<Unknown>";
            ViewBag.Country = country;
            return View();
        }
    
    }
}