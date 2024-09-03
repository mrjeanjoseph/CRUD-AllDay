using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.SPA {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
