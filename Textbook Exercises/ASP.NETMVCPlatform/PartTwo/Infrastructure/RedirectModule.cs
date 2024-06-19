using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PartTwo.Workflow.Infrastructure
{
    public class RedirectModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.MapRequestHandler += (src, agrs) =>
            {
                RouteValueDictionary rvd = app.Context.Request.RequestContext.RouteData.Values;

                if (Compare(rvd, "controller", "Home") && Compare(rvd, "action", "Authenticate"))
                {
                    string url = UrlHelper.GenerateUrl("", "Index", "Home", rvd,
                        RouteTable.Routes, app.Context.Request.RequestContext, false);
                    app.Context.Response.Redirect(url);
                }
            };
        }

        private bool Compare(RouteValueDictionary rvd, string key, string value) =>        
             string.Equals((string)rvd[key], value, StringComparison.OrdinalIgnoreCase);
        

        public void Dispose() { }
    }
}