using IdentityApiSupport.Infrastructure;
using IdentityApiSupport.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdentityApiSupport.Controllers
{
    public class HomeController : Controller
    {
        #region Private Methods
        private AppUser CurrentUser
        {
            get => UserManager.FindByName(HttpContext.User.Identity.Name);
        }

        private AppUserManager UserManager
        {
            get => HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
        }

        private Dictionary<string, object> GetData(string actionName)
        {
            Dictionary<string, object> dictObj = new Dictionary<string, object>
            {
                { "Action", actionName },
                { "User", HttpContext.User.Identity.Name },
                { "Authenticated", HttpContext.User.Identity.IsAuthenticated },
                { "Auth Type", HttpContext.User.Identity.AuthenticationType },
                { "In Users Role", HttpContext.User.IsInRole("Users") },
                { "In Principal Role", HttpContext.User.IsInRole("Principal") },
            };
            return dictObj;
        }
        #endregion

        [Authorize]
        public ActionResult Index()
        {
            return View(GetData("Index"));
        }

        [Authorize(Roles = "Users")]
        public ActionResult OtherAction()
        {
            return View("Index", GetData("OtherAction"));
        }

        [Authorize]
        public ActionResult UserProperties()
        {
            return View(CurrentUser);
        }

        [Authorize, HttpPost]
        public async Task<ActionResult> UserProperties(Cities city)
        {
            AppUser user = CurrentUser;
            user.City = city;
            user.SetCountryFromCity(city);
            await UserManager.UpdateAsync(user);
            return View(user);
        }
    }
}