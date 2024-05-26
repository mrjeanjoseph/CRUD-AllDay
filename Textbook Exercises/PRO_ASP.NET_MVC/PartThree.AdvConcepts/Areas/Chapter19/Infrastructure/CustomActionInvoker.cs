using System.Web.Mvc;

namespace Chapter19.ControllerExtensibility.Infrastructure
{
    public class CustomActionInvoker : IActionInvoker
    {
        public bool InvokeAction(ControllerContext controllerContext, string actionName)
        {
            if (actionName == "Index")
            {
                controllerContext.HttpContext.Response.Write("This is the output from the index action");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}