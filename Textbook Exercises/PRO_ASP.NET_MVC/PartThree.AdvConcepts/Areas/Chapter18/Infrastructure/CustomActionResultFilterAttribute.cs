using System.Web.Mvc;

namespace Chapter18.ApplyingFilters
{
    public class CustomActionResultFilterAttribute 
        : FilterAttribute, IActionFilter, IResultFilter
    {
        public virtual void OnActionExecuting(ActionExecutingContext contextParam) { }              
        public virtual void OnActionExecuted(ActionExecutedContext contextParam) { }              
        public virtual void OnResultExecuting(ResultExecutingContext contextParam) { }              
        public virtual void OnResultExecuted(ResultExecutedContext contextParam) { }
    }
}