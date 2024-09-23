using SportsStore.WebUI.ActionFilter;
using System.Web.Mvc;

namespace SportsStore.WebUI {
    public class FilterConfig {

        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {

            filters.Add(new HandleErrorAttribute());

            filters.Add(new ActionLogFilter());
        }
    }
}
