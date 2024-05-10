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
    }
}