using Filters.Infrastructure;
using System.Web.Mvc;
using System.Web.Security;

namespace Filters.Controllers {
    public class GoogleAccountController : Controller {
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string returnUrl) {
            if(username.EndsWith("@google.com") && password == "secret") {
                FormsAuthentication.SetAuthCookie(username, false);
                return Redirect(returnUrl ?? Url.Action("Index", "Home"));

            } else {
                ModelState.AddModelError("", "Incorrect username or password");
                return View();

            }
        }

        [GoogleAuth]
        public string List() {
            return "This is the list of on the home controller.";
        }
    }
}