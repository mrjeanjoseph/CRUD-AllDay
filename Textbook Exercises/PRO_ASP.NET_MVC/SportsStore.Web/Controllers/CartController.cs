using SportsStore.Domain;
using SportsStore.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IMerchRepo _merchRepository;

        public CartController(IMerchRepo merchRepo)
        {
            _merchRepository = merchRepo;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = GetCart(), ReturnUrl = returnUrl });
        }

        public RedirectToRouteResult AddToCart(int merchId, string returnUrl)
        {
            Merchandise merch = _merchRepository.Merch.FirstOrDefault(m => m.Id == merchId);

            if (merch != null) GetCart().AddItem(merch, 1);

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int merchId, string returnUrl)
        {
            Merchandise merch = _merchRepository.Merch.FirstOrDefault(m => m.Id == merchId);

            if (merch != null) GetCart().RemoveLine(merch);

            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
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