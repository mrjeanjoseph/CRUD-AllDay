using System.Web.Mvc;

namespace EssentialFeatures {
    public class Chapter4AreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Chapter4";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "EssentialFeatures",
                "EssentialFeatures/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}