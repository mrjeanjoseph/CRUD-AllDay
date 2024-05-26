using System;
using System.Web.Mvc;

namespace Chapter18.ApplyingFilters.Controllers
{
    public class BaseFiltersController : Controller
    {
        [Authorize(Users = "Admin")]
        public string Index() => "<h1>This is the Index action Method on the '<em>Applying Filters</em>' Clientele controller</h1>";

        public string CustomFilter() => "<h1>This is a normal filter test method</h1>";


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

        [ProfileAction, ProfileResult, ProfileAll]
        public string ProfileAllFilterTest()
        {
            return "<h1>This is the Profile All (Action and Result) Filter Test</h1>";
        }
    }
}