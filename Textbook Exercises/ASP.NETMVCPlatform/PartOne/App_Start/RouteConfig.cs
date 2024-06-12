using PartOne.Infrastructure;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PartOne
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.Add(new Route("handler/{*path}",
                //new CustomRouteHandler { HandlerType = typeof(DayOfWeekHandler) }));

            routes.IgnoreRoute("handler/{*pathInfo}");

            routes.MapRoute(name: "Default", url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional } );
        }
    }

    internal class CustomRouteHandler : IRouteHandler
    {
        public Type HandlerType { get; set; }

        public IHttpHandler GetHttpHandler(RequestContext requestContext) =>
            (IHttpHandler)Activator.CreateInstance(HandlerType);
    }
}
