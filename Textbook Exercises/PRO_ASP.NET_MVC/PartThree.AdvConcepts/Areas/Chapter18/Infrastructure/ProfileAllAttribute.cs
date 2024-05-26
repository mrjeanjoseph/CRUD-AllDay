using System.Diagnostics;
using System.Web.Mvc;

namespace Chapter18.ApplyingFilters
{
    public class ProfileAllAttribute : CustomActionResultFilterAttribute {
        private Stopwatch _timer;

        public override void OnActionExecuting(ActionExecutingContext contextParam)
        {
                _timer = Stopwatch.StartNew();
        }
        public override void OnResultExecuted(ResultExecutedContext contextParam)
        {
            _timer.Stop();
            contextParam.HttpContext.Response.Write(string
                .Format("<div>Result method elapsed time: {0:F6}</div>",
                _timer.Elapsed.TotalSeconds));
        }
    }
}