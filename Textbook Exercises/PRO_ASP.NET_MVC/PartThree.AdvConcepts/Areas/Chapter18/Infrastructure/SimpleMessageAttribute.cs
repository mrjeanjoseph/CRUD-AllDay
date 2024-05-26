using System;
using System.Web.Mvc;

namespace Chapter18.ApplyingFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple=true)]
    public class SimpleMessageAttribute : FilterAttribute, IActionFilter
    {
        public string simpleMesg { get; set; }
        public void OnActionExecuted(ActionExecutedContext contextParam)
        {
            contextParam.HttpContext.Response.Write(string
                .Format("<div>[After Action: {0}]</div>", simpleMesg));
        }

        public void OnActionExecuting(ActionExecutingContext contextParam)
        {
            contextParam.HttpContext.Response.Write(string
                .Format("<div>[Before Action: {0}]</div>", simpleMesg));
        }
    }
}