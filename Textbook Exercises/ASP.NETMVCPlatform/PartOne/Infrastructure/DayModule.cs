using System;
using System.Web;

namespace PartOne.Infrastructure
{
    public class DayModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.BeginRequest += (src, args) =>
            {
                app.Context.Items["DayModule_Time"] = DateTime.Now;
            };
        }
        public void Dispose()
        {
            // One of these days, we'll do something
        }
    }
}