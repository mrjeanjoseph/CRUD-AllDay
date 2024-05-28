using System.Web.Mvc;

namespace Chapter21.HelperMethods
{
    public class Chapter21AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Chapter21";
            }
        }

        public override void RegisterArea(AreaRegistrationContext routes) 
        {
            routes.MapRoute(
                "",
                "HelperMethods/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "FormRouteOne",
                "app/forms/{controller}/{action}"
            );
        }
    }
}