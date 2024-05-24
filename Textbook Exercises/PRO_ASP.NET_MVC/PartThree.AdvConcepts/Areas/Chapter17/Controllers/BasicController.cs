using System.Web.Mvc;
using System.Web.Routing;

namespace Chapter17.ControllersAndActions.Controllers
{
    public class BasicController : IController
    {
        public void Execute(RequestContext requestContext)
        {
            string controller = (string)requestContext.RouteData.Values["controller"];
            string action = (string)requestContext.RouteData.Values["action"];

            if (action.ToLower() == "redirect")
            {
                requestContext.HttpContext.Response.Redirect("/Derived/Index");
            } 
            else
            {
                string result = string.Format("Controller: {0}, Action: {1}", controller, action);
                requestContext.HttpContext.Response.Write(result);
            }
        }
    }
}