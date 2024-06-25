using CachingContent.Infrastructure;
using System;
using System.Diagnostics;
using System.Threading;
using System.Web;
using System.Web.Mvc;


namespace CachingContent.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(30));
            Response.Cache.SetCacheability(HttpCacheability.Server);
            Response.Cache.AddValidationCallback(CheckCachedItem, Request.UserAgent);


            Thread.Sleep(1000);
            int counterValue = AppStateHelper.IncrementAndGet(AppStateKeys.INDEX_COUNTER);
            Debug.WriteLine(string.Format("INDEX_COUNTER: {0}", counterValue));
            return View(counterValue);
        }

        public void CheckCachedItem(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = data.ToString() == context.Request.UserAgent ? HttpValidationStatus.Valid : HttpValidationStatus.Invalid;
            Debug.WriteLine("Cache Status: " + validationStatus);
        }

        public ActionResult IndexOldTwo()
        {
            if (Request.RawUrl == "/Home/Index")
            {
                Response.Cache.SetNoServerCaching();
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }
            else
            {
                Response.Cache.SetExpires(DateTime.Now.AddSeconds(30));
                Response.Cache.SetCacheability(HttpCacheability.Public);

            }

            Thread.Sleep(1000);
            int counterValue = AppStateHelper.IncrementAndGet(AppStateKeys.INDEX_COUNTER);
            Debug.WriteLine(string.Format("INDEX_COUNTER: {0}", counterValue));
            return View(counterValue);
        }

        [OutputCache(CacheProfile = "cpone")]
        public ActionResult IndexOldOne()
        {
            Thread.Sleep(1000);
            int counterValue = AppStateHelper.IncrementAndGet(AppStateKeys.INDEX_COUNTER);
            Debug.WriteLine(string.Format("INDEX_COUNTER: {0}", counterValue));
            return View(counterValue);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public PartialViewResult GetCurrentTime()
        {
            return PartialView((object)DateTime.Now.ToShortDateString());
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