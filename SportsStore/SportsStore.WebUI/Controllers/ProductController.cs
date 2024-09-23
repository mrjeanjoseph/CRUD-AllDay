using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.ActionFilter;
using SportsStore.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers {

    public class ProductController : Controller {

        private readonly IProductRepository repository;
        private int _pageSize = 4;
        public int PageSize { get { return _pageSize; } set { _pageSize = value; } }

        public ProductController() { }

        public ProductController(IProductRepository productRepository) {

            this.repository = productRepository;
        }

        [ActionLogFilter]
        public ViewResult List(string category, int page = 1) {

            ProductsListViewModel viewModel = new ProductsListViewModel();

            viewModel.Products = repository.Products
                .Where(m => category == null || m.Category == category)
                .OrderBy(m => m.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

            viewModel.PagingInfo = new PagingInfo() {

                TotalItems = repository.Products
                .Where(m => category == null || m.Category == category).Count(),
                ItemsPerPage = PageSize,
                CurrentPage = page
            };

            viewModel.CurrentCategory = category;

            return View(viewModel);
        }

        public FileContentResult GetImage(int id) {

            Product product = repository.Products
                .FirstOrDefault(m => m.ProductID == id);
            if (product != null)
                return File(product.ImageData, product.ImageMimeType);
            else 
                return null;
        }
    }
}
