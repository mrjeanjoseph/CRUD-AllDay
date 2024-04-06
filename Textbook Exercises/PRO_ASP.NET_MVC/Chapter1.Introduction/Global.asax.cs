using System.Web.Mvc;

namespace Chapter1.Introduction {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
        }
    }
}
