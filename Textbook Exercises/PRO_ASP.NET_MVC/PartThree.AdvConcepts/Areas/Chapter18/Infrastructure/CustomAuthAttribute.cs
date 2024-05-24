using System.Web;
using System.Web.Mvc;

namespace Chapter18.ApplyingFilters
{
    public class CustomAuthAttribute : AuthorizeAttribute
    {
        private readonly bool _localAllowed;

        public CustomAuthAttribute(bool allowedParam) => _localAllowed = allowedParam;        

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.IsLocal)
                return _localAllowed;
            else
                return true;
        }
    }
}