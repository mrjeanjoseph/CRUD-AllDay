using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace Chapter18.ApplyingFilters.Controllers
{
    public class HomeController : Controller
    {
        private Stopwatch _timer;

        [CustomAuth(false)]
        public string Index() => "<h1>This is the Index action on the '<em>Applying Filters</em>' Home controller</h1>";        

        [GoogleAuth, Authorize(Users = "Bae@google.com")]
        public string ListFromGoogle()
        {
            return "<h1>This is the '<em>List From Google</em>' action Method</h1>";
        }

        [Authorize(Users = "admin")]
        public string IndexNewUser()
        {
            return "<h1>This is the Index New User action method.</h1>";
        }

        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "RangeError")]
        public string RangeTest(int id)
        {
            if (id > 100)
                return String.Format("The id value is: {0}", id);
            else
                throw new ArgumentOutOfRangeException("id", id, "");
        }

        protected override void OnActionExecuting(ActionExecutingContext contextParam)
        {
            _timer = Stopwatch.StartNew();
        }
        protected override void OnResultExecuted(ResultExecutedContext contextParam)
        {
            _timer.Stop();
            contextParam.HttpContext.Response.Write(string
                .Format("<div>Result method elapsed time: {0:F6}</div>",
                _timer.Elapsed.TotalSeconds));
        }
    }
}