using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.Web.Security;

namespace Chapter18.ApplyingFilters
{
    public class GoogleAuthAttribute : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext contextParam)
        {
            IIdentity ident = contextParam.Principal.Identity;
            if (!ident.IsAuthenticated || !ident.Name.EndsWith("@google.com"))
                contextParam.Result = new HttpUnauthorizedResult();
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext contextParam)
        {
            if (contextParam.Result == null || contextParam.Result is HttpUnauthorizedResult)
            {
                contextParam.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "GoogleAccount" },
                    {"action", "Login" },
                    {"returnUrl", contextParam.HttpContext.Request.RawUrl },
                });
            } else
            {
                FormsAuthentication.SignOut();
            }
        }
    }
}