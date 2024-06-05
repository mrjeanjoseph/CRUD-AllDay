using System.Web.Optimization;

namespace PartFour.AdvConcepts
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/scriptfiles").Include(
                "~/Scripts/jquery-{version}.min.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js"));
        }
    }
}
