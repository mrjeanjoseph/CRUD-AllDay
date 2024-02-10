using System.Diagnostics;
using System.Web.Mvc;

namespace Filters.Infrastructure {
    public class ProfileAllAttribute : ActionFilterAttribute {
        private Stopwatch _timer;

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            _timer = Stopwatch.StartNew();
        }

        public override void OnResultExecuted(ResultExecutedContext resultContext) {
            _timer.Stop();

            string htmlContent = string.Format("<div>Total elapsed time: {0:F6}</div>", _timer.Elapsed.TotalSeconds);
            resultContext.HttpContext.Response.Write(htmlContent);
        }
    }
}