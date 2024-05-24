using System;
using System.Web.Mvc;

namespace Chapter17.ControllersAndActions.Controllers
{
    public class ExampleController : Controller
    {
        public ViewResult Index()
        {
            return View("Homepage");
        }

        public ViewResult IndexDefault()
        {
            return View();
        }

        public ViewResult IndexWithViewName()
        {
            return View("Index", "_AlternateLayoutPage");
        }

        public ViewResult IndexByViewPath()
        {
            return View("~/Views/Other/Index.cshtml");
        }

        public ViewResult IndexViewModelObject()
        {
            DateTime date = DateTime.Now;
            return View(date);
        }
        public ViewResult IndexWithViewBag()
        {
            ViewBag.Message = "Hello";
            ViewBag.CurrentDate = DateTime.Now;
            return View();
        }

        public RedirectResult RedirectUser()
        {
            return Redirect("/Example/Index");
        }

        public RedirectResult RedirectUserPermanently()
        {
            return RedirectPermanent("/Example/Index");
        }

        public RedirectToRouteResult RoutedRedirections()
        {
            return RedirectToRoute(new
            {
                controller = "Example",
                action = "Index",
                Id = "MyId"
            });
        }

        public RedirectToRouteResult RedirectToActionMethod()
        {
            // Current controller
            return RedirectToAction("Index");

            // Diff controller
            //return RedirectToAction("Index", "Basic");
        }

        public RedirectToRouteResult PreservingDataAcrossRedirection()
        {
            TempData["Message"] = "This is a temporary message";
            TempData["CurrentDate"] = DateTime.Now;
            return RedirectToAction("Index");
        }

        public ViewResult IndexWithTempData()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Date = TempData["Date"];
            return View("Index");
        }

        public HttpStatusCodeResult SpecificResultCode()
        {
            return new HttpStatusCodeResult(404, "Sit sa a wap cheche ya, pa gen sevis ladann");
        }

        public HttpStatusCodeResult StatusCode404()
        {
            return HttpNotFound("Hello");
        }

        public HttpStatusCodeResult StatusCode401()
        {
            return new HttpUnauthorizedResult();
        }
    }
}