using System;
using System.Web;

namespace PartOne.Infrastructure
{
    public class DayModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.BeginRequest += (src, args) => {
                if (app.Context.Handler is IRequiresDate)
                    app.Context.Items["DayModule_Time"] = DateTime.Now;
            };
        }
        public void Dispose()
        {
            // One of these days, we'll do something
        }
    }
}