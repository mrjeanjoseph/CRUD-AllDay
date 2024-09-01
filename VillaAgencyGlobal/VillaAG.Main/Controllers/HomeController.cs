using System.Web.Mvc;
using VillaAG.Main.DataRepository;

namespace VillaAG.Main.Controllers {
    public class HomeController : Controller {
        // Sample data

        private readonly InformationBannerRepository _infoBanner = null;

        public HomeController()
        {
            _infoBanner = new InformationBannerRepository();
        }

        public ActionResult Index() => View(_infoBanner.GetAllInformationBanners());

        public ActionResult Contact() {
            return View();
        }

        public ActionResult Properties() {
            return View();
        }

        public ActionResult PropertyDetails() {
            return View();
        }
    }
}