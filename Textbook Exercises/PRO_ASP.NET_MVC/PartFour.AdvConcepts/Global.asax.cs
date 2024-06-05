using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;

namespace PartFour.AdvConcepts
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Register Web API configuration
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //ValueProviderFactories.Factories.Insert(0, new CustomValueProviderFactory());
            //ModelBinders.Binders.Add(typeof(AddressSummary), new AddressSummaryBinder());
        }
    }
}
