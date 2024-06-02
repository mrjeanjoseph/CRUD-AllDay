using System.Web.Mvc;

namespace Chapter25.ModelValidation
{
    public class Chapter25AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get { return "Chapter25"; }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "",
                "ModelValidation/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}