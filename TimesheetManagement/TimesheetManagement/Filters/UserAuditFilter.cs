using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using TimesheetManagement.Infrastructure;
using TimesheetManagement.Application;
using TimesheetManagement.Models;

namespace TimesheetManagement.Filters
{
    public class UserAuditFilter : ActionFilterAttribute
    {
        IAudit _IAudit;
        public UserAuditFilter()
        {
            _IAudit = new AuditConcrete();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            AuditTB objaudit = new AuditTB();
            var actionName = filterContext.ActionDescriptor?.DisplayName ?? string.Empty;
            var controllerName = filterContext.RouteData.Values["controller"]?.ToString() ?? string.Empty;
            var httpContext = filterContext.HttpContext;
            var session = httpContext.Session;

            if (session.GetInt32("UserID") != null)
            {
                objaudit.UserID = session.GetInt32("UserID").ToString();
            }
            else if (session.GetInt32("AdminUser") != null)
            {
                objaudit.UserID = session.GetInt32("AdminUser").ToString();
            }
            else
            {
                objaudit.UserID = string.Empty;
            }

            objaudit.SessionID = httpContext.TraceIdentifier;
            objaudit.IPAddress = httpContext.Connection.RemoteIpAddress?.ToString();
            objaudit.PageAccessed = httpContext.Request.Path + httpContext.Request.QueryString;
            objaudit.LoggedInAt = DateTime.Now;
            if (actionName.Contains("LogOff"))
            {
                objaudit.LoggedOutAt = DateTime.Now;
            }
            objaudit.LoginStatus = "A";
            objaudit.ControllerName = controllerName;
            objaudit.ActionName = actionName;
            objaudit.UrlReferrer = httpContext.Request.Headers["Referer"].ToString();
            _IAudit.InsertAuditData(objaudit);
        }
    }
}