using SportsStore.Domain;
using SportsStore.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.Web.Controllers
{
    public class MerchController : Controller
    {
        private readonly IMerchRepo _repository;
        public int PageSize = 5;

        public MerchController(IMerchRepo merchRepo)
        {
            _repository = merchRepo;
        }

        public ActionResult List_old()
        {
            return View(_repository.Merch);
        }

        public ViewResult List(int page = 1)
        {
            MerchListViewModel viewModel = new MerchListViewModel
            {
                Merchandises = _repository.Merch
                .OrderBy(m => m.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Merch.Count()
                }
            };
            return View(viewModel);
            //return View(_repository.Merch
            //    .OrderBy(m => m.Id)
            //    .Skip((page - 1) * PageSize)
            //    .Take(PageSize));
        }
    }
}