using System.Web.Mvc;

namespace Ch24_MvcModels.Infrastructure {
    public class CustomValueProviderFactory : ValueProviderFactory {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext) {
            return new CountryValueProvider();
        }
    }
}