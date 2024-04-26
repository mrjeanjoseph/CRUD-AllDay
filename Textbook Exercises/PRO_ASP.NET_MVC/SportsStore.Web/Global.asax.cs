using SportsStore.Domain;
using SportsStore.Web.Infrastructure;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
