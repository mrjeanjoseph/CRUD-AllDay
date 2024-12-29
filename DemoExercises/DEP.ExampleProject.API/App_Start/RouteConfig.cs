using System.Web.Mvc;
using System.Web.Routing;

namespace DEP.ExampleProject.API {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "swagger/ui/index",
                defaults: new { id = UrlParameter.Optional }
            );
        }
    }
}
