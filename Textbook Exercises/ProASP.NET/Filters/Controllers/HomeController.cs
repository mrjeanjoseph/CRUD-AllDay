using System;
using System.Web.Mvc;
using Filters.Infrastructure;

namespace Filters.Controllers {
    public class HomeController : Controller {
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
        [ProfileAction]
        public string FilterTest() {
            return "This is the Filter Test Action";
        }
    }
}