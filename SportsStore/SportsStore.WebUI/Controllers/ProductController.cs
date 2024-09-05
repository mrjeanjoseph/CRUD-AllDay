using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers {
    public class ProductController : Controller {

        private IProductsRepository repository;
        private int _pageSize = 4;
        public int pageSize { get { return _pageSize; } set { _pageSize = value; } }

        public ProductController(IProductsRepository productRepository) {
            this.repository = productRepository;
        }

        public ViewResult List(string category, int page = 1) {

            //return View(repository.Products.OrderBy(m => m.ProductID)
            //    .Skip((page - 1) * pageSize).Take(pageSize));

            ProductsListViewModel viewModel = new ProductsListViewModel();

            viewModel.Products = repository.Products.Where(m => category == null || m.Category == category)
                .OrderBy(m => m.ProductID).Skip((page - 1) * pageSize).Take(pageSize);

            viewModel.PagingInfo = new PagingInfo() {TotalItems = repository.Products
                .Where(m => category == null || m.Category == category).Count(),

                ItemsPerPage = pageSize,
                CurrentPage = page
            };

            viewModel.CurrentCategory = category;
            return View(viewModel);
        }
    }
}
