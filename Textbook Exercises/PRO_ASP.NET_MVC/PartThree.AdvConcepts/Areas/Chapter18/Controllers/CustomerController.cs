using System.Web.Mvc;

namespace Chapter18.ApplyingFilters.Controllers
{
    [SimpleMessage(simpleMesg = "The Letter A")]
    public class CustomerController : Controller
    {
        [SimpleMessage(simpleMesg = "The Letter A")]
        [SimpleMessage(simpleMesg = "The Letter B")]
        public string Index() =>
            "<h1>This is the Index action on the '<em>Global Filters</em>' Customer controller</h1>";

        [SimpleMessage(simpleMesg = "The Letter A", Order = 1)]
        [SimpleMessage(simpleMesg = "The Letter B", Order = 2)]
        public string WhenOrderingIsNeeded() =>
            "<h2>This is the WhenOrderingIsNeeded action method on the '<em>Global Filters</em>' Customer controller</h2>";

        [SimpleMessage(simpleMesg = "The Letter B")]
        public string ClassAndMethodFilters() =>
            "<h2>This is the '<em>ClassAndMethodFilters action method</em>' on the Global Filters Customer controller</h2>";

        [CustomOverrideActionFilters]
        [SimpleMessage(simpleMesg = "The Letter B")]
        public string OverridingFilters() =>
            "<h2>This is the '<em>OverridingFilters action method</em>' on the Global Filters Customer controller</h2>";
    }
}