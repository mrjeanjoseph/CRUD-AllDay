using System.Web.Mvc;
using System.Web.Routing;
using System.Web;

namespace PlatformServices.Configuration
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
