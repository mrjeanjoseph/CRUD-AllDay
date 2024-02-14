using System;
using System.Diagnostics;
using System.Web.Mvc;
using Filters.Infrastructure;

namespace Filters.Controllers {
    public class HomeController : Controller {
        private Stopwatch _timer;
        // GET: Home

        //[CustomAuth(true)]
        [Authorize(Users = "admin")]
        public string Index() {
            return "If you're seeing the page, then you are authenticated.";
        }

        [GoogleAuth][Authorize(Users = "rhj@google.com")]
        public string List() {
            return "This is the List action on the home controller";
        }

        //[RangeException]
        [HandleError(ExceptionType  = typeof(ArgumentOutOfRangeException),
            View = "RangeError")]
        public string RangeTest(int id) {
            if( id > 100) {
                return string.Format("The id value is: {0}", id);
            } else {
                throw new ArgumentOutOfRangeException("id", id, "");
            }
        }

        //[CustomAction]
        //[ProfileAction]
        //[ProfileResult]
        //[ProfileAll]
        public string FilterTest() {
            return "This is the Filter Test Action";
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext) {
            _timer = Stopwatch.StartNew();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext) {
            _timer.Stop();

            string htmlContent = string.Format("<div>In Controller Total elapsed time: {0:F6}</div>", _timer.Elapsed.TotalSeconds);
            filterContext.HttpContext.Response.Write(htmlContent);
        }
    }
}