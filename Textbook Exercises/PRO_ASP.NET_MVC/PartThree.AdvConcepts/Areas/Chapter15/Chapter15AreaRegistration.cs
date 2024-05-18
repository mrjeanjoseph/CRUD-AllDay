using Chapter15.URLsAndRoutes.Infrastructure;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace Chapter15.URLsAndRoutes
{

    public class Chapter15AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Chapter15";
            }
        }

        public override void RegisterArea(AreaRegistrationContext routes)
        {
            routes.MapRoute("ChromeRoute", "{*catchall}",
                new { Controller = "Home", action = "Index"},
                new { customConstraint = new UserAgentConstraint("Chrome")},
                new[] { "Chapter15.URLsAndRoutes.AnotherControllers" });

            routes.MapRoute("MyRoute", "URLsAndRoutes/{controller}/{action}/{id}/{*catchall}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { controller = "^H.*", action = "^Index$|^About$",
                      httpMethod = new HttpMethodConstraint("GET"),
                      //id = new RangeRouteConstraint(10, 20) },
                      id = new CompoundRouteConstraint(
                          new IRouteConstraint[]
                          {
                              new AlphaRouteConstraint(),
                              new MinLengthRouteConstraint(6)
                          })},
                new[] { "Chapter15.URLsAndRoutes.Controllers" });

            routes.MapRoute("YetOtherRoute", "URLsAndRoutes/{controller}/{action}/{id}/{*catchall}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Chapter15.URLsAndRoutes.AnotherControllers" })
                .DataTokens["UseNamespaceFallback"] = false;

            routes.MapRoute("AnotherRoute", "URLsAndRoutes/{controller}/{action}/{id}/{*catchall}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Chapter15.URLsAndRoutes.AnotherControllers" });

            routes.MapRoute("", "URLsAndRoutes/{controller}/{action}/{id}/{*catchall}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Chapter15.URLsAndRoutes.Controllers" });
        }

        public void RegisterAreaOld(AreaRegistrationContext routes)
        { // This is old code - We're not using this anywhere --

            routes.MapRoute("ShopSchema2", "Shop/OldAction",
                new { Controller = "Home", Action = "Index" });

            routes.MapRoute("ShopSchema", "Shop/{action}",
                new { Controller = "Home" });

            routes.MapRoute("", "VarEl{controller}/{action}");

            routes.MapRoute("", "Public/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("URLsAndRoutes", "URLsAndRoutes/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

    }
}