using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;

namespace TimesheetManagement.Filters
{
    public class ValidateSuperAdminSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                int? superId = null;
                if (filterContext.HttpContext.Session.TryGetValue("SuperAdmin", out var bytes))
                {
                    superId = BitConverter.ToInt32(bytes, 0);
                }
                if (superId == null)
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