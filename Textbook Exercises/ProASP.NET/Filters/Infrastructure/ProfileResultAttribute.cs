using System.Diagnostics;
using System.Web.Mvc;

namespace Filters.Infrastructure {
    public class ProfileResultAttribute : FilterAttribute, IResultFilter {
        private Stopwatch _timer;

        public void OnResultExecuting(ResultExecutingContext filterContext) {
            _timer = Stopwatch.StartNew();
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) {
            _timer.Stop();
            string htmlContent = string.Format("<div>Action method elapsed time: {0:F6}</div>", _timer.Elapsed.TotalSeconds);
            filterContext.HttpContext.Response.Write(htmlContent);
        }

    }
}