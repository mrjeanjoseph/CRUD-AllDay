using CreatingStatefulData.Infrastructure;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Mvc;

namespace CreatingStatefulData.Controllers
{
    public class ChachingTechniqueController : Controller
    {
        // GET: ChachingTechnique
        public ActionResult Index()
        {
            SelfExpiringData<long?> selfExpData = (SelfExpiringData<long?>)HttpContext.Cache["pageLength"];
            return View(selfExpData == null ? null : selfExpData.Value);
            //return View(selfExpData ?? null);  // could use this one too for short
        }

        [HttpPost]
        public async Task<ActionResult> PopulateCache()
        {
            HttpResponseMessage result = await new HttpClient().GetAsync("https://www.apress.com/us");

            long? data = result.Content.Headers.ContentLength;

            SelfExpiringData<long?> selfExpData = new SelfExpiringData<long?>(data, 3);
            HttpContext.Cache.Insert("pageLength", selfExpData, selfExpData,
                Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, HandleNotification);

            return RedirectToAction("Index");
        }

        private void HandleNotification(string key, CacheItemUpdateReason reason, 
            out object data, out CacheDependency dependency, 
            out DateTime absoluteExpiration, out TimeSpan slidingExpiration)
        {
            Debug.WriteLine("Item {0} removed. ({1})",
                key, Enum.GetName(typeof(CacheItemRemovedReason), reason));

            data = dependency = new SelfExpiringData<long?>(GetData(false).Result, 3);

            slidingExpiration = Cache.NoSlidingExpiration;
            absoluteExpiration = Cache.NoAbsoluteExpiration;
        }

        private async Task<long?> GetData(bool awaitConn = true)
        {
            HttpResponseMessage response = await new
                HttpClient().GetAsync("https://www.apress.com/us").ConfigureAwait(awaitConn);

            return response.Content.Headers.ContentLength;
        }

        [HttpPost]
        public async Task<ActionResult> PopulateCacheOldSix()
        {
            HttpResponseMessage result = await new HttpClient().GetAsync("https://www.apress.com/us");

            long? data = result.Content.Headers.ContentLength;

            SelfExpiringData<long?> selfExpData = new SelfExpiringData<long?>(data, 3);
            HttpContext.Cache.Insert("pageLength", selfExpData, selfExpData,
                Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, 
                CacheItemPriority.Normal, HandleNotification);

            return RedirectToAction("Index");
        }

        private void HandleNotification(string key, object data, CacheItemRemovedReason reason)
        {
            Debug.WriteLine("Item {0} removed. ({1})", 
                key, Enum.GetName(typeof(CacheItemRemovedReason), reason));
        }

        [HttpPost]
        public async Task<ActionResult> PopulateCacheOldFive()
        {
            HttpResponseMessage result = await new HttpClient().GetAsync("https://www.apress.com/us");

            long? data = result.Content.Headers.ContentLength;

            SelfExpiringData<long?> selfExpData = new SelfExpiringData<long?>(data, 3);
            CacheDependency fileDep = new CacheDependency(Request.MapPath("~/DataCaching.txt"));
            AggregateCacheDependency aggDep = new AggregateCacheDependency();
            aggDep.Add(selfExpData, fileDep);
            HttpContext.Cache.Insert("pageLength", selfExpData, aggDep);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> PopulateCacheOldFour()
        {
            HttpResponseMessage result = await new HttpClient().GetAsync("https://www.apress.com/us");

            long? data = result.Content.Headers.ContentLength;

            SelfExpiringData<long?> selfExpData = new SelfExpiringData<long?>(data, 3);
            HttpContext.Cache.Insert("pageLength", selfExpData, selfExpData);

            return RedirectToAction("Index");
        }

        public ActionResult IndexOldOne()
        {
            var currentPageLength = (HttpContext.Cache["pageLength"]);
            return View((long?)currentPageLength);
        }

        [HttpPost]
        public async Task<ActionResult> PopulateCacheOldThree()
        {
            HttpResponseMessage result = await new HttpClient().GetAsync("https://www.apress.com/us");

            long? data = result.Content.Headers.ContentLength;

            CacheDependency dependency = new CacheDependency(Request.MapPath("~/DataCaching.txt"));
            HttpContext.Cache.Insert("pageLength", data, dependency);

            DateTime timestamp = DateTime.Now;
            CacheDependency tsDependency = new CacheDependency(null, new string[] { "pageLength" });
            HttpContext.Cache.Insert("pageLength", data, tsDependency);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> PopulateCacheOldTwo()
        {
            HttpResponseMessage result = await new HttpClient().GetAsync("https://www.apress.com/us");

            long? data = result.Content.Headers.ContentLength;
            CacheDependency dependency = new CacheDependency(Request.MapPath("~/DataCaching.txt"));
            HttpContext.Cache.Insert("pageLength", data, dependency);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> PopulateCacheOldOne()
        {
            HttpResponseMessage result = await new HttpClient().GetAsync("https://www.apress.com/us");

            long? data = result.Content.Headers.ContentLength;
            TimeSpan idleDuration = new TimeSpan(0, 0, 30);
            HttpContext.Cache.Insert("pageLength", data, null, Cache.NoAbsoluteExpiration, idleDuration);

            //DateTime expiryTime = DateTime.Now.AddSeconds(30);
            //HttpContext.Cache.Insert("pageLength", data, null, expiryTime, Cache.NoSlidingExpiration);
            //HttpContext.Cache["pageLength"] = result.Content.Headers.ContentLength;
            return RedirectToAction("Index");
        }
    }
}