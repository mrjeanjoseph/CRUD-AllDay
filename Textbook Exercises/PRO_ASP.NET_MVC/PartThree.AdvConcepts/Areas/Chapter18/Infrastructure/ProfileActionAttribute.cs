using System.Diagnostics;
using System.Web.Mvc;

namespace Chapter18.ApplyingFilters
{
    public class ProfileActionAttribute : FilterAttribute, IActionFilter
    {
        private Stopwatch _timer;

        public void OnActionExecuting(ActionExecutingContext contextParam)
        {
            _timer = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext contextParam)
        {
            _timer.Stop();
            if (contextParam.Exception == null)            
                contextParam.HttpContext.Response.Write(string
                    .Format("<div>Action method elapsed time: {0:F6}</div>", _timer.Elapsed.TotalSeconds));
            
        }
    }
}