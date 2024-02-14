using Filters.Infrastructure;
using System.Web.Mvc;

namespace Filters {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection Ch19_Filters) {
            Ch19_Filters.Add(new HandleErrorAttribute());
            Ch19_Filters.Add(new ProfileAllAttribute());
        }
    }
}