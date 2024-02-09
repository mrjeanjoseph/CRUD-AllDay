using System;
using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure {
    public class CustomAuthAttribute : AuthorizeAttribute {
        private readonly bool localAllowed;

        public CustomAuthAttribute(bool allowedParam) {
            this.localAllowed = allowedParam;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext) {
            if (httpContext.Request.IsLocal) {
                return localAllowed;
            } else { return true; }
        }
    }

    public class RangeExceptionAttribute {
        public void OnException(ExceptionContext filterContext) {
            if(!filterContext.ExceptionHandled && 
                    filterContext.Exception is ArgumentOutOfRangeException) {
                filterContext.Result = new RedirectResult("~/Content/RangeErrorPage.html");
                filterContext.ExceptionHandled = true;
            }
        }
    }
}