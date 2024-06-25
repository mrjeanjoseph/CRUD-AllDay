using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CachingContent
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            if (custom == "mobile")
                return Request.Browser.IsMobileDevice.ToString();
            else return base.GetVaryByCustomString(context, custom);
        }
    }
}
