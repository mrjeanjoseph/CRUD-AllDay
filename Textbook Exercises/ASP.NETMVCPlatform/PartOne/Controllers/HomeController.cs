using PartOne.SimpleApp.Models;
using System.Web.Mvc;

namespace PartOne.SimpleApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Color color)
        {
            Color? oldColor = Session["color"] as Color?;
            if (oldColor != null)
                Votes.ChangeVote(color, (Color)oldColor);
            else Votes.RecordVote(color);

            ViewBag.SelectedColor = Session["color"] = color;
            return View();
        }
    }
}