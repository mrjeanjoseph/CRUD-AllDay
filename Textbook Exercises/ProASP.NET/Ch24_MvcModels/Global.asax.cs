using Ch24_MvcModels.Infrastructure;
using Ch24_MvcModels.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ch24_MvcModels {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Both of these throw an error in the controller
            //ValueProviderFactories.Factories.Insert(0, new CustomValueProviderFactory());
            //ValueProviderFactories.Factories.Add(new CustomValueProviderFactory());

            ModelBinders.Binders.Add(typeof(AddressSummary), new AddressSummaryBinder());
        }
    }
}
