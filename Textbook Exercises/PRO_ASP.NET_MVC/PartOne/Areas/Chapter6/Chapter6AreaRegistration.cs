using System.Web.Mvc;

namespace EssentialTools
{
    public class Chapter6AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Chapter6";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Chapter6_default",
                "Chapter6/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}