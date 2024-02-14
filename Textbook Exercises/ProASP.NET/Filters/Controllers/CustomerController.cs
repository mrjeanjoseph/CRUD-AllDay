using Ch19_Filters.Infrastructure;
using System.Web.Mvc;

namespace Ch19_Filters.Controllers {

    [SimpleMessage(Message = "A")]
    public class CustomerController : Controller {
        // GET: Customer

        //[SimpleMessage(Message = "A", Order = 2)]
        //[SimpleMessage(Message = "B", Order = 1)]
        public string Index() {
            return "This is THE INDEX section of the customer controller";
        }

        [CustomOverrideActionFilters]
        [SimpleMessage(Message ="B")]
        public string AnotherIndex() {
            return "This is ANOTHER INDEX section of the customer controller";
        }
    }
}