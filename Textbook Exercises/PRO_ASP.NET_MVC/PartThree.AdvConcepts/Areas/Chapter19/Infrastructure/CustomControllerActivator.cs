using Chapter19.ControllerExtensibility.Controllers;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Chapter19.ControllerExtensibility.Infrastructure
{
    public class CustomControllerActivator : IControllerActivator
    {
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == typeof(CommonItemsController))
                controllerType = typeof(PremiumItemsController);

            return (IController)DependencyResolver.Current.GetService(controllerType);
        }
    }
}