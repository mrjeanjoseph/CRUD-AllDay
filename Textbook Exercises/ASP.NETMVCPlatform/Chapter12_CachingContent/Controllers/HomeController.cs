using CachingContent.Infrastructure;
using System.Diagnostics;
using System.Threading;
using System.Web.Mvc;
using System.Web.UI;


namespace CachingContent.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        [OutputCache(Duration = 30, Location = OutputCacheLocation.Downstream)]
        public ActionResult Index()
        {
            Thread.Sleep(1000);
            int counterValue = AppStateHelper.IncrementAndGet(AppStateKeys.INDEX_COUNTER);
            Debug.WriteLine(string.Format("INDEX_COUNTER: {0}", counterValue));
            return View(counterValue);
        }

        public ActionResult detail()
        {
            
            if (HttpContext.Application["isButtonClicked"] == null)
                HttpContext.Application["isButtonClicked"] = false;

            ViewBag.IsButtonClicked = HttpContext.Application["isButtonClicked"];
            return View("Result");
        }

        [HttpPost]
        public ActionResult Detail()
        {
            bool isButtonClicked = (bool)HttpContext.Application["isButtonClicked"];
            isButtonClicked = !isButtonClicked;

            HttpContext.Application["isButtonClicked"] = isButtonClicked;
            ViewBag.IsButtonClicked = HttpContext.Application["isButtonClicked"];

            return detail();
        }
    }
}