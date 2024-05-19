using System.Web.Mvc;

namespace Chapter14.OverviewOfMVC
{
    public class Chapter14AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Chapter14";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "",
                "Chapter14/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}