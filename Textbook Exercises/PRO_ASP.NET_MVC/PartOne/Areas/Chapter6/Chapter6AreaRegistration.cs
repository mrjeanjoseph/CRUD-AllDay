using System.Web.Mvc;

namespace EssentialTools {
    public class Chapter6AreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Chapter6";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "EssentialTools",
                "EssentialTools/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}