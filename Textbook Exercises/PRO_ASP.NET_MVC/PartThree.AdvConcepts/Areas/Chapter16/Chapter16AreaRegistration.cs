using System.Web.Mvc;

namespace Chapter16.URLsAndRoutes
{
    public class Chapter16AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get { return "Chapter16"; }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("Default_Route", "Ch16URLsAndRoutes/{controller}/{action}/{id}",
                new {controller ="Home", action="Index", id=UrlParameter.Optional },
                new[] { "Chapter16.URLsAndRoutes.Controllers" });
        }

        //There is an optimization package that needs to be downloaded.
    }
}