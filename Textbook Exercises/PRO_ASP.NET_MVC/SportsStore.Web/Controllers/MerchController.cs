using SportsStore.Domain;
using SportsStore.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.Web.Controllers
{
    public class MerchController : Controller
    {
        private readonly IMerchandiseRepository _merchRepo;
        public int PageSize = 5;

        public MerchController(IMerchandiseRepository merchRepo)
        {
            _merchRepo = merchRepo;
        }

        public ActionResult List_old()
        {
            return View(_merchRepo.Merchandises);
        }

        public ViewResult List(string category, int page = 1)
        {
            MerchListViewModel viewModel = new MerchListViewModel
            {
                Merchandises = _merchRepo.Merchandises
                .Where(m => category == null || m.Category == category)
                .OrderBy(m => m.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                    _merchRepo.Merchandises.Count() :
                    _merchRepo.Merchandises.Where(m => m.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(viewModel);
        }
    }
}