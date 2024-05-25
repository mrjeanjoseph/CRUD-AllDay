using System.Web.Mvc;

namespace Chapter18.ApplyingFilters
{
    public class CustomActionAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext contextParam)
        {
            if (contextParam.HttpContext.Request.IsLocal)
                contextParam.Result = new HttpNotFoundResult();
        }

        public void OnActionExecuted(ActionExecutedContext contextParam)
        {
            // Doing nothing here.
        }
    }
}