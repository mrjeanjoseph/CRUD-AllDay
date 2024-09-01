using System.Web.Optimization;

namespace VillaAG.Main {
    public class BundleConfig {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {

            bundles.Add(new Bundle("~/bundles/script-asset").Include(
                      "~/Scripts/jquery.min.js",
                      "~/Scripts/bootstrap.bundle.min.js",
                      "~/Scripts/isotope.min.js",
                      "~/Scripts/owl-carousel.js",
                      "~/Scripts/counter.js",
                      "~/Scripts/properties.js",
                      "~/Scripts/site.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css-asset").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/fontawesome.css",
                      "~/Content/site.css",
                      "~/Content/owl.css",
                      "~/Content/animate.css",
                      "~/Content/swiper-bundle.min.css"
            ));
        }
    }
}
