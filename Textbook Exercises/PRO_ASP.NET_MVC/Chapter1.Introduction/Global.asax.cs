using Chapter2.Introduction;
using System.Web.Mvc;
using System.Web.Routing;

namespace Chapter1.Introduction {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
