using System;
using System.Web;
using System.Web.Routing;

namespace PartTwo.Workflow.Infrastructure
{
    public class HandlerSelectionModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.PostResolveRequestCache += (src, agrs) =>
            {
                RouteValueDictionary rvd = app.Context.Request.RequestContext.RouteData.Values;

                if (!Compare(rvd, "controller", "Home"))
                {
                    app.Context.RemapHandler(new InfoHandler());
                }
            };
        }
        private bool Compare(RouteValueDictionary rvd, string key, string value) =>
             string.Equals((string)rvd[key], value, StringComparison.OrdinalIgnoreCase);

        public void Dispose() { }
    }
}