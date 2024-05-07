using SportsStore.Domain;
using SportsStore.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;
        public int PageSize = 4;
        public ProductController(IProductRepository productRepo)
        {
            _repository = productRepo;
        }

        // GET: Product
        public ViewResult ProductListing(string category, int page = 1)
        {
            ProductsListViewModel viewModel = new ProductsListViewModel
            {
                Products = _repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Products.Count()
                },
                CurrentCategory = category
            };

            return View(viewModel);
        }
    }
}