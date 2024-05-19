using Chapter16.URLsAndRoutes;
using System.Web.Mvc;
using System.Web.Routing;

namespace AdvConcepts {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Content/{filename}.html");

            routes.RouteExistingFiles = true;
            routes.MapMvcAttributeRoutes();

            routes.Add(new Route("SayHello", new CustomRouteHandler()));

            routes.Add(new LegacyRoute(
                "~/NewController/NewFinalLink",
                "~/OldController/OldFinalLink"));

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "AdvConcepts.Controllers" }
            );
        }
    }
}
