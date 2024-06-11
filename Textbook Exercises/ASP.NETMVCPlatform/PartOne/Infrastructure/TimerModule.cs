using System;
using System.Diagnostics;
using System.Web;

namespace PartOne.Infrastructure
{
    public class TimerModule : IHttpModule
    {
        private Stopwatch _timer;

        public void Init(HttpApplication context)
        {
            context.BeginRequest += HandleEvent;
            context.EndRequest += HandleEvent;
        }

        private void HandleEvent(object src, EventArgs args)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx.CurrentNotification == RequestNotification.BeginRequest)
                _timer = Stopwatch.StartNew();
            else            
                ctx.Response.Write(string.Format("<div class='alert alert-success'>Elapsed: {0:F5} seconds</div>", ((float)_timer.ElapsedTicks) / Stopwatch.Frequency));
            
        }

        public void Dispose()
        {
            // Nothing to do here yet
        }
    }
}