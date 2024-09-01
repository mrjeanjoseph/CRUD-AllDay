using System.Web.Mvc;

namespace VillaAG.Main {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }

    public class CheckAjaxRequestAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {

            bool isAjaxRequest = filterContext.HttpContext.Request.IsAjaxRequest();
            string redirectUrl = string.Format("{0}://{1}/",
                filterContext.HttpContext.Request.Url.Scheme,
                filterContext.HttpContext.Request.Url.Authority);

            if (!isAjaxRequest) filterContext.Result = new RedirectResult(redirectUrl);
        }
    }
}
