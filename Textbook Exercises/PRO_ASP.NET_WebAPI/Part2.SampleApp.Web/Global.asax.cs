using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Part2.SampleApp.Web
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
