using PartOne.SimpleApp.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PartOne.SimpleApp.Controllers
{
    public class HomeOldController : Controller
    {
        // GET: Home
        public ActionResult IndexOld()
        {
            //return View(HttpContext.Application["events"]);
            return View(GetTimeStamps());
        }

        private List<string> GetTimeStamps()
        {
            return new List<string> {
                string.Format("Application timestamp: {0}", HttpContext.Application["app_timestamp"]),
                string.Format("Request timestamp: {0}", Session["request_timestamp"]),
            };
        }

        [HttpPost]
        public ActionResult IndexOld(Color color)
        {
            Color? oldColor = Session["color"] as Color?;
            if (oldColor != null)
                Votes.ChangeVote(color, (Color)oldColor);
            else Votes.RecordVote(color);

            ViewBag.SelectedColor = Session["color"] = color;
            //return View(HttpContext.Application["events"]);
            return View(GetTimeStamps());
        }
    }
}