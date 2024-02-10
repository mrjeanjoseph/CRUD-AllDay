using Filters.Infrastructure;
using System.Web.Mvc;

namespace Filters.Controllers {
    public class CustomerController : Controller {
        // GET: Customer

        [SimpleMessage(Message = "A", Order = 2)]
        [SimpleMessage(Message = "B", Order = 1)]
        public string Index() {
            return "This is the customer controller";
        }
    }
}