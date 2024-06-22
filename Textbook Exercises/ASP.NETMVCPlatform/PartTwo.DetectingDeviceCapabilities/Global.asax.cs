using DetectingDeviceCapabilities.Infrastructure;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;

namespace DetectingDeviceCapabilities
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //HttpCapabilitiesBase.BrowserCapabilitiesProvider = new KindleCapabilities();

            DisplayModeProvider.Instance.Modes.Insert(0,
                new DefaultDisplayMode("Safari")
                {
                    //ContextCondition = ctx => ctx.Request.Browser.IsBrowser("Safari"),
                    ContextCondition = ctx => ctx.Request.Browser.IsMobileDevice
                });
        }
    }
}
