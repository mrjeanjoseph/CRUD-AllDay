using System;
using System.Diagnostics;
using System.Web;

namespace PartOne.Infrastructure
{
    public class RequestTimerEventArgs : EventArgs
    {
        public float Duriation { get; set; }
    }
    public class TimerModule : IHttpModule
    {
        public event EventHandler<RequestTimerEventArgs> RequestTimed;
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
            {
                float duration = ((float)_timer.ElapsedTicks) / Stopwatch.Frequency;

                ctx.Response.Write(string.Format("<div class='alert alert-success'>Elapsed: {0:F5} seconds</div>", ((float)_timer.ElapsedTicks) / Stopwatch.Frequency));

                if(RequestTimed != null)
                    RequestTimed(this, new RequestTimerEventArgs { Duriation = duration });

                //RequestTimed?.Invoke(this, new RequestTimerEventArgs { Duriation = duration });
            }
            
        }

        public void Dispose()
        {
            // Nothing to do here yet
        }
    }
}