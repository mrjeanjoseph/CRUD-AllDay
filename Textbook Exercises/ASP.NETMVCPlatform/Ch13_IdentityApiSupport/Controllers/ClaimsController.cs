using IdentityApiSupport.Infrastructure;
using System.Security.Claims;
using System.Web.Mvc;

namespace IdentityApiSupport.Controllers
{
    public class ClaimsController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                return View("Error", new string[] { "No Claims available" });
            else return View(identity.Claims);
        }


        //[Authorize(Roles = "HT-Staff")]
        [ClaimsAccess(Issuer ="RemoteClaims", ClaimType = ClaimTypes.PostalCode, Value = "HT 011509")]
        public string OtherAction()
        {
            return "This is a Protected Action";
        }
    }
}