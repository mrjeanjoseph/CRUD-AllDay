using System.Web.Mvc;

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

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "URLsAndRoutes",
                "URLsAndRoutes/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}