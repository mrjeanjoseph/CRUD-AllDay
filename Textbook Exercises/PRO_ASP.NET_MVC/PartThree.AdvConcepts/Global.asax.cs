using Chapter20.RazorPagesAndViewEngine.Infrastructure;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AdvConcepts
{
    public class MvcApplication : HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomLocationViewEngine());

            //ControllerBuilder.Current.SetControllerFactory(new
            //DefaultControllerFactory(new CustomControllerActivator()));

            //ViewEngines.Engines.Clear();
            //ViewEngines.Engines.Add(new DebugDataViewEngine());
        }
    }
}
