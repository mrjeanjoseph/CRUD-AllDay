using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Chapter24.ModelBinding.Infrastructure;
using Chapter24.ModelBinding.Models;

namespace PartFour.AdvConcepts
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //ValueProviderFactories.Factories.Insert(0, new CustomValueProviderFactory());
            //ModelBinders.Binders.Add(typeof(AddressSummary), new AddressSummaryBinder());
        }
    }
}
