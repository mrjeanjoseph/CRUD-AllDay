using System.Web.Mvc;

namespace Chapter20.RazorViewEngine
{
    public class Chapter20AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Chapter20";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Chapter20_default",
                "Chapter20/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}