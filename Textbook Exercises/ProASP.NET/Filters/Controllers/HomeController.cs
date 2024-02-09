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
    }
}