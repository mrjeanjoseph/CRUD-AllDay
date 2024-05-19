using System.Web.Mvc;

namespace Chapter16.URLsAndRoutes
{
    public class Chapter16AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Chapter16"; }
        }

        public override void RegisterArea(AreaRegistrationContext routes)
        {
            routes.MapRoute("DiskFile", "Areas/Chapter16/StaticContent.html",
                new { controller = "Customer", action = "List" });

            routes.MapRoute("MyRoute", "{controller}/{action}");
            routes.MapRoute("MyOtherRoute", "App/{action}", new { controller = "Customer" });

            routes.MapRoute("Default_Route", "Ch16URLsAndRoutes/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        //There is an optimization package that needs to be downloaded.
    }
}