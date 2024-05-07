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
                new { controller = "Product", action = "ProductListing", category = (string)null, page = 1 });

            routes.MapRoute(
                null,
                "Page{page}",
                new { controller = "Product", action = "ProductListing", category = (string)null },
                new { page = @"\d+" });

            routes.MapRoute(
                null,
                "{category}",
                new { controller = "Product", action = "ProductListing", page = 1 });

            routes.MapRoute(
                null,
                "{category}/Page{page}",
                new { controller = "Product", action = "ProductListing", },
                new { page = @"\d+" });

            routes.MapRoute(null, "{controller}/{action}");

        }
    }
}
