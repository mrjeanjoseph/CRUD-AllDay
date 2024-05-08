using SportsStore.Domain;
using System;
using System.Web.Mvc;

namespace SportsStore.Web.HtmlHelpers
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";
        public object BindModel(ControllerContext controllerContext, 
            ModelBindingContext bindingContext)
        {
            //Get the Cart from the session
            Cart cart = null;

            if (controllerContext.HttpContext.Session != null)
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];

            //Create the Cart if there's none in the session data
            if (cart == null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                    controllerContext.HttpContext.Session[sessionKey] = cart;
            }
            // return cart to be used.
            return cart;
        }
    }
}