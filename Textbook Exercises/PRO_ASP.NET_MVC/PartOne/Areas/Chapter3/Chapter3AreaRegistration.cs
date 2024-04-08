using System.Web.Mvc;

namespace Chapter2.TheMVCPattern {
    public class Chapter3AreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Chapter3";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "TheMVCPattern",
                "TheMVCPattern/{controller}/{action}/{id}",
                new { Controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}