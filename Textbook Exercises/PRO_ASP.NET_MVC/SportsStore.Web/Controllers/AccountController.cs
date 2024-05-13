using SportsStore.Web.Infrastructure;
using SportsStore.Web.Models;
using System.Web.Mvc;

namespace SportsStore.Web.Controllers
{
    public class AccountController : Controller
    {
        readonly IAuthProvider _authProvider;

        public AccountController(IAuthProvider auth) => _authProvider = auth;

        public ViewResult Login() => View();
        

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authProvider.Authenticate(viewModel.UserName, viewModel.Password))
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                else
                {
                    ModelState.AddModelError("", "Incorrect username and password");
                    return View();
                }
            } else            
                return View();            
        }
    }
}