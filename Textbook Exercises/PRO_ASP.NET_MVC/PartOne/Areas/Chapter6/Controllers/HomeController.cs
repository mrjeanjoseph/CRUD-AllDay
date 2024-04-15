using EssentialTools.Models;
using System.Web.Mvc;
using Ninject;

namespace EssentialTools.Controllers {
    public class HomeController : Controller {
        private readonly IValueCalculator _calc;

        private readonly Merchandise[] merchandises = {
            new Merchandise {Name = "Bouret", Category = "Biznis Nasyonal", Price = 1050},
            new Merchandise {Name = "Barik Pistash", Category = "Biznis Lokal", Price = 3800},
            new Merchandise {Name = "Pye Palmis", Category = "Biznis Lokal", Price = 2000},
            new Merchandise {Name = "Konbit pou Rekolt", Category = "Biznis Lokal", Price = 500},
            new Merchandise {Name = "Sevis Transpo", Category = "Biznis Nasyonal", Price = 250},
        };

        public HomeController(IValueCalculator calcParam, IValueCalculator calcParam2) {
            _calc = calcParam;
        }

        public ActionResult Index() {

            #region with DI implementation, we no longer need these
            //Archived -- These get replace with Ninject DI
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();

            IValueCalculator calc_old3 = ninjectKernel.Get<IValueCalculator>();
            //IValueCalculator calc_old2 = new LinqValueCalculator();
            //LinqValueCalculator calc_old1 = new LinqValueCalculator();
            #endregion

            ShoppingCart cart = new ShoppingCart(_calc) { merchandises =  merchandises };
            decimal totalValue = cart.CalculateMerchandiseTotal();
            return View(totalValue);
        }
    }
}