using Chapter19.ControllerExtensibility.Infrastructure;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AdvConcepts.NotApplicable
{ //
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);//
            ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory());

            ControllerBuilder.Current.DefaultNamespaces.Add("MyControllerNamesspace");
            ControllerBuilder.Current.DefaultNamespaces.Add("OtherNamespace.*");
        }
    }
}
