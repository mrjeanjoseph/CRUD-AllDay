using DetectingDeviceCapabilities.Infrastructure;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace DetectingDeviceCapabilities
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //HttpCapabilitiesBase.BrowserCapabilitiesProvider = new KindleCapabilities();
        }
    }
}
