using System;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace LoggingRequests.Infrastructure
{
    public class LogModule_OldOne : IHttpModule
    {
        private static int sharedCounter = 0;
        private int requestCounter;

        private static object lockOject = new object();
        private Exception requestException = null;

        public void Init(HttpApplication app)
        {
            app.BeginRequest += (src, args) => requestCounter = ++sharedCounter;

            app.Error += (src, args) => requestException = HttpContext.Current.Error;

            app.LogRequest += (src, args) => WriteLogMessage(HttpContext.Current);

        }

        private void WriteLogMessage(HttpContext ctx)
        {
            StringWriter sr = new StringWriter();
            sr.WriteLine("-----------------------");
            sr.WriteLine("Request: {0} for {1}", requestCounter, ctx.Request.RawUrl);
            if (ctx.Handler != null) sr.WriteLine("Handler: {0}", ctx.Handler.GetType());

            sr.WriteLine("Status Code: {0}, Message: {1}",
                ctx.Response.StatusCode, ctx.Response.StatusDescription);
            sr.WriteLine("Elapsed Time: {0} ms", DateTime.Now.Subtract(ctx.Timestamp).Milliseconds);

            if (requestException != null) sr.WriteLine("Error: {0}", requestException.GetType());
            
            lock(lockOject) Debug.Write(sr.ToString());
        }

        public void Dispose() { /*Maybe we will do something here */ }

    }
}