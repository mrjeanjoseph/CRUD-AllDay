using System;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.Web.Security;

namespace Filters.Infrastructure {
    public class GoogleAuthAttribute : FilterAttribute, IAuthorizationFilter {
        public void OnAuthorization(AuthorizationContext filterContext) {
            throw new NotImplementedException();
        }

        public void OnAuthentication(AuthenticationContext filterContext) {
            IIdentity ident = filterContext.Principal.Identity;
            if(!ident.IsAuthenticated || !ident.Name.EndsWith("@google.com")) {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext context) {
            if (context.Result == null || context.Result is HttpUnauthorizedResult) {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    {"controller", "GoogleAccount"},
                    {"action", "Login" },
                    {"returnUrl", context.HttpContext.Request.Path}
                });
            } else {
                FormsAuthentication.SignOut();
            }
        }
    }
}