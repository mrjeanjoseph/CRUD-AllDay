using IdentityApiSupport.Infrastructure;
using IdentityApiSupport.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdentityApiSupport.Controllers
{
    public class AdminController : Controller
    {
        private AppUserManager UserManager
        {
            get => HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.FullName, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if(result.Succeeded) return RedirectToAction("Index", "Admin");
                else AddErrorsFromResult(result);
            }
            return View(model);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
                ModelState.AddModelError("", error);
            
        }
    }
}