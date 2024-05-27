using System.Web.Mvc;

namespace Chapter20.RazorPagesAndViewEngine
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
                "",
                "RazorPagesAndViewEngine/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}