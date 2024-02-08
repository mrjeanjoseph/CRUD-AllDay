using System;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace Filters.Infrastructure {
    public class GoogleAuthAttribute : FilterAttribute, IAuthorizationFilter {
        public void OnAuthorization(AuthorizationContext filterContext) {
            throw new NotImplementedException();
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext context) {
            if (context.Result == null) {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    {"controller", "GoogleAccount"},
                    {"action", "Login" },
                    {"returnUrl", context.HttpContext.Request.Path}
                });
            }
        }
    }
}