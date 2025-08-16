using DPE.Application.Interfaces;
using System.Web.Mvc;

namespace DPE.Main.Controllers
{
    public class DebugController : Controller
    {
        private readonly IPersonService _personService;

        public DebugController(IPersonService personService)
        {
            _personService = personService;
        }

        public ActionResult Ping()
        {
            var result = _personService.GetAllAsync(); // Should not throw
            return Content("DI is working!");
        }

        public ActionResult Index()
        {
            var people = _personService.GetAllAsync();
            return Json(people, JsonRequestBehavior.AllowGet);
        }

    }
}