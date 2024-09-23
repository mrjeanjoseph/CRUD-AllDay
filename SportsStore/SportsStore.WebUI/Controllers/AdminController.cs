using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers {

    //[Authorize]
    public class AdminController : Controller {

        private readonly IProductRepository repo;

        public AdminController(IProductRepository repo) {
            this.repo = repo;
        }

        public ViewResult Edit(int productId) {
            Product product = repo.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        // GET: Admin
        public ViewResult Index() {
            return View(repo.Products);
        }

        [HttpPost]
        public ActionResult Edit(Product product) {
            if (ModelState.IsValid) {
                repo.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            } else {
                // there is something wrong with the data values                
                return View(product);
            }
        }

        public ActionResult Create() {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId) {
            Product p = repo.DeleteProduct(productId);
            if (p != null) {
                TempData["message"] = string.Format("{0} has been deleted", p.Name);
            }
            return RedirectToAction("Index");
        }

        public ActionResult ActionLog() {
            using (EFDbContext dbcontext = new EFDbContext()) {

                var actionLogs = (from al in dbcontext.ActionLogs select al).ToList();

                return View(actionLogs);
            }
        }
    }
}
