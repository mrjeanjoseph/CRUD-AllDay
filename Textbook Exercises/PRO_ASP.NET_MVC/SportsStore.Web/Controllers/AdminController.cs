using SportsStore.Domain;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMerchandiseRepository _repository;

        public AdminController(IMerchandiseRepository repoParam)
        {
            _repository = repoParam;
        }
        // GET: Admin
        public ViewResult Index()
        {
            return View(_repository.Merchandises);
        }

        public ViewResult Edit(int id)
        {
            Merchandise merchandise = _repository.Merchandises
                .FirstOrDefault(m => m.Id == id);
            return View(merchandise);
        }

        [HttpPost]
        public ActionResult Edit(Merchandise merch)
        {
            if(ModelState.IsValid)
            {
                _repository.SaveMerchandise(merch);
                TempData["message"] = string.Format("{0} has been saved", merch.Id);
                return RedirectToAction("Index");
            } else  // There is something wrong with the data values
            {
                return View(merch);
            }
        }
    }
}