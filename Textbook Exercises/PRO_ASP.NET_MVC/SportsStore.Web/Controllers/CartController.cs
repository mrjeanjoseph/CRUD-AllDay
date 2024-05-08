using SportsStore.Domain;
using SportsStore.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IMerchandiseRepository _merchRepo;
        private readonly IOrderProceessor _orderprocessor;


        public CartController(IMerchandiseRepository merchRepo, IOrderProceessor orderprocessor)
        {
            _merchRepo = merchRepo;
            _orderprocessor = orderprocessor;
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
        { //adding the ? temporarily b/c the id is somehow not being passed in.
            Merchandise merch = _merchRepo.Merchandises.FirstOrDefault(m => m.Id == Id);

            if (merch != null) cart.AddItem(merch, 1);

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int Id, string returnUrl)
        {
            Merchandise merch = _merchRepo.Merchandises.FirstOrDefault(m => m.Id == Id);

            if (merch != null) cart.RemoveLine(merch);

            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingdetails)
        {
            if (cart.Lines.Count() == 0)
                ModelState.AddModelError("", "Sorry, your cart is empty");

            if (ModelState.IsValid)
            {
                _orderprocessor.ProcessOrder(cart, shippingdetails);
                cart.Clear();
                return View("Completed");
            }
            else
                return View(shippingdetails);
        }
    }
}