using SportsStore.Domain;
using SportsStore.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.Web.Controllers
{
    public class ProductVendorController : Controller
    {
        private readonly IProductVendorRepo _repository;
        public int PageSize = 5;

        public ProductVendorController(IProductVendorRepo VendorRepo)
        {
            _repository = VendorRepo;
        }

        public ActionResult List_old()
        {
            return View(_repository.ProductVendor);
        }

        public ViewResult List(int page = 1)
        {
            ProductVendorListViewModel viewModel = new ProductVendorListViewModel
            {
                ProductVendors = _repository.ProductVendor
                .OrderBy(m => m.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.ProductVendor.Count()
                }
            };
            return View(viewModel);
        }
    }
}