using SportsStore.Domain;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.Web.Controllers
{
    //[Authorize]
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

        public ViewResult Create()
        {
            return View("Edit", new Merchandise());
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
                TempData["message"] = string.Format("Changes to {0} details has been saved", merch.Name);
                return RedirectToAction("Index");
            } else  // There is something wrong with the data values
            {
                return View(merch);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Merchandise deleteMerch = _repository.DeleteMerchandise(id);
            if(deleteMerch != null)
                TempData["message"] = string.Format("{0} is removed from the list of Merchandise", deleteMerch.Name);
            return RedirectToAction("Index");
        }
    }
}