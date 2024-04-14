using EssentialTools.Models;
using System.Web.Mvc;

namespace EssentialTools.Controllers {
    public class HomeController : Controller {

        private readonly Merchandise[] merchandises = {
            new Merchandise {Name = "Bouret", Category = "Biznis Nasyonal", Price = 1050},
            new Merchandise {Name = "Barik Pistash", Category = "Biznis Lokal", Price = 3800},
            new Merchandise {Name = "Pye Palmis", Category = "Biznis Lokal", Price = 2000},
            new Merchandise {Name = "Konbit pou Rekolt", Category = "Biznis Lokal", Price = 500},
            new Merchandise {Name = "Sevis Transpo", Category = "Biznis Nasyonal", Price = 250},
        };

        public ActionResult Index() {
            IValueCalculator calc = new LinqValueCalculator();

            //Archived
            LinqValueCalculator calc_old = new LinqValueCalculator();

            ShoppingCart cart = new ShoppingCart(calc) { merchandises =  merchandises };

            decimal totalValue = cart.CalculateMerchandiseTotal();

            return View(totalValue);
        }
    }
}