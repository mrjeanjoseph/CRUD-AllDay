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
        public int PageSize = 5;
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
                    TotalItems = category == null ?
                    _repository.Products.Count() : 
                    _repository.Products.Where(e => e.Category == category)
                    .Count()
                },
                CurrentCategory = category
            };

            return View(viewModel);
        }
    }
}