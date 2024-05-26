using System;
using Chapter19.ControllerExtensibility.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Chapter19.ControllerExtensibility.Infrastructure
{
    public class CustomControllerFactory : IControllerFactory
    {
        public IController CreateController(
            RequestContext requestContext, string controllerName)
        {
            Type targetType = null;
            switch(controllerName)
            {
                case "CommonItems": targetType = typeof(CommonItemsController); break;
                case "PremiumItems": targetType = typeof(PremiumItemsController); break;
                default:
                    requestContext.RouteData.Values["controller"] = "CommonItems";
                    targetType = typeof(CommonItemsController); break;
            }

            return targetType == null ? null :
                (IController)DependencyResolver.Current.GetService(targetType);
        }

        public SessionStateBehavior GetControllerSessionBehavior(
            RequestContext requestContext, string controllerName)
        {
            switch (controllerName)
            {
                case "Home": return SessionStateBehavior.ReadOnly;
                case "Premium": return SessionStateBehavior.Required;
                default:return SessionStateBehavior.Default;
            }
            
        }

        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;

            //disposable?.Dispose();

            if (disposable != null)
                disposable.Dispose();
            
        }
    }
}