using System.Web.Mvc;

namespace WorkingWithRazor {
    public class Chapter5AreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Chapter5";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "WorkingWithRazor",
                "Chapter5/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}