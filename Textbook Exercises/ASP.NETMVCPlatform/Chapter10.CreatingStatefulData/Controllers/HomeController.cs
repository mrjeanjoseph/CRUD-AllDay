using CreatingStatefulData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CreatingStatefulData.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Dictionary<string, object> dataOject = new Dictionary<string, object>();
            //dataOject.Add("Placeholder Property", "Placeholder Value");
            dataOject.Add("Counter", AppStateHelper.Get(AppStateKeys.COUNTER, 0));
            IDictionary<AppStateKeys, object> stateData = AppStateHelper
                .GetMulitple(AppStateKeys.LAST_REQUEST_TIME, AppStateKeys.LAST_REQUEST_URL);
            foreach (AppStateKeys key in stateData.Keys)
                dataOject.Add(Enum.GetName(typeof(AppStateKeys), key), stateData[key]);

            return View(dataOject);
        }

        public ActionResult Increment()
        {
            int currentValue = (int)AppStateHelper.Get(AppStateKeys.COUNTER, 0);
            AppStateHelper.Set(AppStateKeys.COUNTER, currentValue + 1);
            AppStateHelper.SetMultiple(new Dictionary<AppStateKeys, object>
            {
                {AppStateKeys.LAST_REQUEST_TIME, HttpContext.Timestamp },
                {AppStateKeys.LAST_REQUEST_URL, Request.RawUrl},
            });
            return RedirectToAction("Index");
        }
    }
}