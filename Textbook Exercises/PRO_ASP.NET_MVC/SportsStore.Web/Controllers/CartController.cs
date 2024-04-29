using SportsStore.Domain;
using SportsStore.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IMerchandiseRepository _merchRepo;

        public CartController(IMerchandiseRepository merchRepo)
        {
            _merchRepo = merchRepo;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int merchId, string returnUrl)
        { //adding the ? temporarily b/c the id is somehow not being passed in.
            Merchandise merch = _merchRepo.Merchandises.FirstOrDefault(m => m.Id == merchId);

            if (merch != null) cart.AddItem(merch, 1);

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart( Cart cart, int merchId, string returnUrl)
        {
            Merchandise merch = _merchRepo.Merchandises.FirstOrDefault(m => m.Id == merchId);

            if (merch != null) cart.RemoveLine(merch);

            return RedirectToAction("Index", new { returnUrl });
        }

        //private Cart GetCart() {
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart == null) {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}
    }
}