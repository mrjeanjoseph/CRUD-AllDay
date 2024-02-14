using Filters.Infrastructure;
using System.Web.Mvc;

namespace Filters {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection Filters) {
            Filters.Add(new HandleErrorAttribute());
            Filters.Add(new ProfileAllAttribute());
        }
    }
}