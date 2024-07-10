using PingYourPackage.WebAPI;
using System;
using System.Web;
using System.Web.Http;

namespace PingYourPackage.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var config = GlobalConfiguration.Configuration;

            //RouteConfig.RegisterRoutes(config);
            //WebAPIConfig.Configure(config);
            AutofacWebAPI.Initialize(config);
        }
    }
}
