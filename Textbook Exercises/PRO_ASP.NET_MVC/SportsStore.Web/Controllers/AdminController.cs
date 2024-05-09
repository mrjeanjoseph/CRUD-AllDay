using SportsStore.Domain;
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
        public ActionResult Index()
        {
            return View(_repository.Merchandises);
        }
    }
}