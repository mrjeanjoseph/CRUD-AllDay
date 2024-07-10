using PingYourPackage.Domain;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using WebApiDoodle.Web;
using WebApiDoodle.Web.MessageHandlers;

namespace PingYourPackage.WebAPI
{
    public class PingYourPackageAuthHandler : BasicAuthenticationHandler
    {
        protected override Task<IPrincipal> AuthenticateUserAsync(
            HttpRequestMessage request, string username,
            string password, CancellationToken cancellationToken)
        {
            var membershipService = request.GetMembershipService();

            var validUserCtx = membershipService.ValidateUser(username, password);

            return Task.FromResult(validUserCtx.Principal);
        }
    }

    internal static class HttpRequestMessageExtensions
    {
        internal static IMembershipService GetMembershipService(this HttpRequestMessage request) =>
            request.GetService<IMembershipService>();

        private static TService GetService<TService> (this HttpRequestMessage requestMessage)
        {
            IDependencyScope dependencyScope = requestMessage.GetDependencyScope();

            TService service = (TService)dependencyScope.GetService(typeof(TService));

            return service;
        }
    }
}
