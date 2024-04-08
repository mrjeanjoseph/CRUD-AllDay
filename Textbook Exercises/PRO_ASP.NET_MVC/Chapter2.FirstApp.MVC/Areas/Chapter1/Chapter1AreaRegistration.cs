using System.Web.Mvc;

namespace Chapter2.FirstApp.MVC.Areas.Chapter1 {
    public class Chapter1AreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Chapter1";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "Chapter1",
                "Chapter1/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}