using System;
using System.Web.Mvc;

namespace Chapter18.ApplyingFilters.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuth(false)]
        public string Index()
        {
            return "<h1>This is the Index action on the '<em>Applying Filters</em>' Home controller</h1>";
        }

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

        [RangeException]
        public string RangeTestOld(int id)
        {
            if (id > 100)
                return String.Format("The id value is: {0}", id);
            else
                throw new ArgumentOutOfRangeException("id", id, "");
        }

        [CustomAction]
        public string CustomFilterTest()
        {
            return "<h1>This is a Custom Filter Test</h1>";
        }

        [ProfileAction]
        public string ProfileActionFilterTest()
        {
            return "<h1>This is the Profile Filter Test</h1>";
        }

        [ProfileAction, ProfileResult]
        public string ProfileResultFilterTest()
        {
            return "<h1>This is the Profile Filter Test</h1>";
        }
    }
}