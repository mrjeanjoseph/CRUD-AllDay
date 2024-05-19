using Microsoft.Ajax.Utilities;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Chapter16.URLsAndRoutes
{
    public class LegacyRoute : RouteBase
    {
        private readonly string[] _urls;

        public LegacyRoute(params string[] targetUrls)
        {
            _urls = targetUrls;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData result = null;

            string requestedUrl = httpContext.Request.AppRelativeCurrentExecutionFilePath;
            if(_urls.Contains(requestedUrl, StringComparer.OrdinalIgnoreCase))
            {
                result = new RouteData(this, new MvcRouteHandler());
                result.Values.Add("controller", "Legacy");
                result.Values.Add("action", "GetLegacyURL");
                result.Values.Add("legacyURL", requestedUrl);
            }
            return result;
        }

        public override VirtualPathData GetVirtualPath(
            RequestContext requestContext, 
            RouteValueDictionary values)
        {
            VirtualPathData result = null;

            if(values.ContainsKey("legacyURL") &&
                _urls.Contains((string)values["legacyUrl"], StringComparer.OrdinalIgnoreCase))
            {
                result = new VirtualPathData(this, new UrlHelper(requestContext).
                    Content((string)values["legacyURL"]).Substring(1));
            }
            return result;
        }
    }
}