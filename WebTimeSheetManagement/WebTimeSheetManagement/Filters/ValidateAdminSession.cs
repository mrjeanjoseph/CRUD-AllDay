using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;

namespace WebTimeSheetManagement.Filters
{
    public class ValidateAdminSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                int? adminId = null;
                if (filterContext.HttpContext.Session.TryGetValue("AdminUser", out var bytes))
                {
                    adminId = BitConverter.ToInt32(bytes, 0);
                }
                if (adminId == null)
                {
                    if (filterContext.Controller is Controller c)
                    {
                        c.TempData["ErrorMessage"] = "Session has been expired please Login";
                    }
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "Error" }));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}