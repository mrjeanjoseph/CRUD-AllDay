using System.Web.Mvc;

namespace Chapter1.Introduction {
    public class Chapter1AreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Chapter1";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "Introduction",
                "Introduction/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}