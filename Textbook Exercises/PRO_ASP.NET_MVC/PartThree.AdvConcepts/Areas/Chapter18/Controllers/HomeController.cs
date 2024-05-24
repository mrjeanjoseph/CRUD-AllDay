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

        [Authorize(Users = "admin")]
        public string IndexNewUser()
        {
            return "<h1>This is the Index New User action method.</h1>";
        }
    }
}