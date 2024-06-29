using System.Runtime.Remoting.Contexts;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IdentityApiSupport.Infrastructure
{
    public class ClaimsAccessAttribute : AuthorizeAttribute
    {
        public string Issuer { get; set; }
        public string ClaimType { get; set; }
        public string Value { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userIdentity = httpContext.User.Identity;
            return userIdentity.IsAuthenticated &&
                userIdentity is ClaimsIdentity && 
                ((ClaimsIdentity)userIdentity).HasClaim
                (x => x.Issuer == Issuer && x.Type == ClaimType && x.Value == Value);
        }
    }

}