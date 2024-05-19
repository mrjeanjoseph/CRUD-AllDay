using System.Web.Mvc;

namespace Chapter15.URLsAndRoutes.Controllers
{
    //[RouteArea("TempController")]
    [RoutePrefix("Users")]
    public class CustomerController : Controller
    {
        [Route("TempActionResult")] // Can't seem to get this one to work.
        public ActionResult Index()
        {
            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.Action = "Index";

            return View("ActionName");
        }

        public ActionResult IndexTwo()
        {
            // Get the controller name
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            // Get the action name
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();

            // Resolve the view path
            ViewEngineResult viewResult = ViewEngines.Engines.FindView(this.ControllerContext, actionName, null);
            string viewPath = viewResult.View != null ? Server.MapPath(viewResult.View.ToString()) : "View not found";

            // Pass the data to the view
            ViewBag.Controller = controllerName;
            ViewBag.Action = viewPath;

            return View("Index");
        }


        [Route("Add/{user}/{id:int}")]
        public string Create(string user, int id)
        {
            return string.Format("Create Method -User: {0}, Id: {1}", user, id);
        }

        [Route("Add/{user}/{password:alpha:length(6)}")]
        public string ChangePass(string user, string password)
        {
            return string.Format("Change Pass Method - User: {0}, Pass: {1}", user, password);
        }

        public ActionResult List()
        {
            ViewBag.Controller = "Customer";
            ViewBag.Action = "Customer List";

            return View("ActionName");
        }
    }
}