using System.Web.Mvc;

namespace Chapter24.ModelBinding.Infrastructure
{
    public class CustomValueProviderFactor : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            throw new System.NotImplementedException();
        }
    }
}