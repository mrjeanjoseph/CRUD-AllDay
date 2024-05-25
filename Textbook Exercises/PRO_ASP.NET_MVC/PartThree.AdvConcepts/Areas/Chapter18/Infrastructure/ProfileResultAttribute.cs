using System.Diagnostics;
using System.Web.Mvc;

namespace Chapter18.ApplyingFilters
{
    public class ProfileResultAttribute : FilterAttribute, IResultFilter
    {
        private Stopwatch _timer;

        public void OnResultExecuting(ResultExecutingContext contextParam)
        {
            _timer = Stopwatch.StartNew();
        }

        public void OnResultExecuted(ResultExecutedContext contextParam)
        {
            _timer.Stop();        
            contextParam.HttpContext.Response.Write(string
                .Format("<div>Result method elapsed time: {0:F6}</div>",
                _timer.Elapsed.TotalSeconds));
            
        }
    }
}