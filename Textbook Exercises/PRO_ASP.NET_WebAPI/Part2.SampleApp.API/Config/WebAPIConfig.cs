using PingYourPackage.ApiModel;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Metadata.Providers;
using System.Web.Http.ModelBinding;
using System.Web.Http.Validation;
using WebApiDoodle.Web.Filters;

namespace PingYourPackage.WebAPI
{
    public class WebAPIConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            //Message Handlers
            config.MessageHandlers.Add(new RequireHttpsMessageHandler());

            config.MessageHandlers.Add(new PingYourPackageAuthHandler());

            config.ParameterBindingRules.Insert(0, descriptor => typeof(IRequestCommand)
            .IsAssignableFrom(descriptor.ParameterType) ? new FromUriAttribute().GetBinding(descriptor) : null);

            //Filters
            config.Filters.Add(new InvalidModelStateFilterAttribute());

            //Formatters
            var jqueryFormatter = config.Formatters.FirstOrDefault(
                x => x.GetType() == typeof(JQueryMvcFormUrlEncodedFormatter));

            config.Formatters.Remove(config.Formatters.FormUrlEncodedFormatter);

            config.Formatters.Remove(jqueryFormatter);

            foreach (var formatter in config.Formatters)
                formatter.RequiredMemberSelector = new SuppressedRequiredMemberSelector();

            //Default Services
            config.Services.Replace(typeof(IContentNegotiator),
                new DefaultContentNegotiator(excludeMatchOnTypeOnly: true));

            config.Services.RemoveAll(typeof(ModelValidatorProvider),
                validator => !(validator is DataAnnotationsModelMetadataProvider));

        }
    }

    internal class SuppressedRequiredMemberSelector : IRequiredMemberSelector
    {
        public bool IsRequiredMember(MemberInfo member) => false;
    }
}
