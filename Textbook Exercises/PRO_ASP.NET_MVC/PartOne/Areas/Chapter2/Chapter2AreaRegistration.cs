using System.Web.Mvc;

namespace Chapter2.PartyInvites {
    public class Chapter2AreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Chapter2";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "PartyInvites",
                "PartyInvites/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}