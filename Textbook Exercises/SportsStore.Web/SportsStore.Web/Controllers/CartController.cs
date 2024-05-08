using SportsStore.Domain;
using SportsStore.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepo;

        public CartController(IProductRepository productParam)
        {
            _productRepo = productParam;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _productRepo.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null) cart.AddItem(product, 1);
            
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _productRepo.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null)            
                cart.RemoveItem(product);
            
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart() //No longer use
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}