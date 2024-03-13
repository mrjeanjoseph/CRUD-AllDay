using Ch24_MvcModels.Infrastructure;
using System.Web.Mvc;

namespace Ch24_MvcModels.Models {

    //[Bind(Include="City
    
    //[ModelBinder(typeof(AddressSummaryBinder))]

    public class AddressSummary {
        public string City {  get; set; }
        public string Country { get; set; }
    }
}