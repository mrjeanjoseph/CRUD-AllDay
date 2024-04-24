using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null,
                "",
                new { controller = "Merch", action = "List", category = (string)null, page = 1 });

            routes.MapRoute(
                null,
                "Page{page}",
                new { controller = "Merch", action = "List", category = (string)null },
                new { page = @"\d+" });

            routes.MapRoute(
                null,
                "{category}",
                new { controller = "Merch", action = "List", page = 1 });

            routes.MapRoute(
                null,
                "{category}/Page{page}",
                new { controller = "Merch", action = "List", },
                new { page = @"\d+" });

            routes.MapRoute(null, "{controller}/{action}");

        }
    }
}
