using System;
using System.Web.Mvc;

namespace Ch19_Filters.Infrastructure {

    //[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SimpleMessageAttribute : FilterAttribute, IActionFilter {
        public string Message { get; set; }
        public void OnActionExecuting(ActionExecutingContext filterContext) {
            string htmlContent = string.Format("<div>Before Action: {0}</div>", Message);
            filterContext.HttpContext.Response.Write(htmlContent);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
            string htmlContent = string.Format("<div>After Action: {0}</div>", Message);
            filterContext.HttpContext.Response.Write(htmlContent);
        }
    }
}