using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;

namespace WebTimeSheetManagement.Filters
{
    public class ValidateUserSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                int? userId = null;
                if (filterContext.HttpContext.Session.TryGetValue("UserID", out var bytes))
                {
                    userId = BitConverter.ToInt32(bytes, 0);
                }
                if (userId == null)
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