using System.Web.Mvc;

namespace Chapter17.ControllersAndActions
{
    public class Chapter17AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get { return "Chapter17"; }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "",
                "ControllersAndActions/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional } );
        }
    }
}