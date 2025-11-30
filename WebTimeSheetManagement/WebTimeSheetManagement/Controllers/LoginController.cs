using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Helpers;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Controllers
{
    public class LoginController : Controller
    {
        private ILogin _ILogin;
        private IAssignRoles _IAssignRoles;
        private ICacheManager _ICacheManager;
        public LoginController()
        {
            _ILogin = new LoginConcrete();
            _IAssignRoles = new AssignRolesConcrete();
            _ICacheManager = new CacheManager();
        }

        [HttpGet]
        public ActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                // Captcha removed (CaptchaMvc not available in ASP.NET Core). Add ASP.NET Core compatible captcha later.
                if (!string.IsNullOrEmpty(loginViewModel.Username) && !string.IsNullOrEmpty(loginViewModel.Password))
                {
                    var Username = loginViewModel.Username;
                    var password = loginViewModel.Password; // TODO: reintroduce encryption if needed.
                    var result = _ILogin.ValidateUser(Username, password);
                    if (result != null)
                    {
                        if (result.RegistrationID <= 0)
                        {
                            ViewBag.errormessage = "Entered Invalid Username and Password";
                        }
                        else
                        {
                            var roleId = result.RoleID.GetValueOrDefault();
                            HttpContext.Session.SetInt32("RoleID", roleId);
                            HttpContext.Session.SetString("Username", result.Username ?? string.Empty);
                            if (roleId == 1)
                            {
                                HttpContext.Session.SetInt32("AdminUser", result.RegistrationID);
                                if (result.ForceChangePassword == 1) return RedirectToAction("ChangePassword", "UserProfile");
                                return RedirectToAction("Dashboard", "Admin");
                            }
                            else if (roleId == 2)
                            {
                                if (!_IAssignRoles.CheckIsUserAssignedRole(result.RegistrationID))
                                {
                                    ViewBag.errormessage = "Approval Pending";
                                    return View(loginViewModel);
                                }
                                HttpContext.Session.SetInt32("UserID", result.RegistrationID);
                                if (result.ForceChangePassword == 1) return RedirectToAction("ChangePassword", "UserProfile");
                                return RedirectToAction("Dashboard", "User");
                            }
                            else if (roleId == 3)
                            {
                                HttpContext.Session.SetInt32("SuperAdmin", result.RegistrationID);
                                return RedirectToAction("Dashboard", "SuperAdmin");
                            }
                        }
                    }
                    else
                    {
                        ViewBag.errormessage = "Entered Invalid Username and Password";
                        return View(loginViewModel);
                    }
                }
                return View(loginViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
