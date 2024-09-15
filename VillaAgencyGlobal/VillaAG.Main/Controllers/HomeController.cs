using System.Web.Mvc;
using VillaAG.Main.DataRepository;
using VillaAG.Main.Infrastructure;

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

        public FileActionResults PropertyReportOne() {
            return new FileActionResults("ReportOne.pdf", "~/Content/Assets/", "application/pdf");
        }

        public FileActionResults PropertyReportTwo() {
            return new FileActionResults("ReportTwo.pdf", "~/Content/Assets/", "application/pdf");
        }
    }
}