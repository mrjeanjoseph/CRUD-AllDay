using System;
using System.Web.Mvc;
using System.Web.Security;

namespace PartThree.AdvConcepts.Areas.Chapter18.Controllers
{
    public class AccountController : Controller
    {
        // GET: Chapter18/Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, Obsolete]
        public ActionResult Login(string username, string password, string returnurl)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return Redirect(returnurl ?? Url.Action("Index", "Admin"));
            } 
            else
            {
                ModelState.AddModelError("", "Incorrect Username or password");
                return View();
            }            
        }
    }
}