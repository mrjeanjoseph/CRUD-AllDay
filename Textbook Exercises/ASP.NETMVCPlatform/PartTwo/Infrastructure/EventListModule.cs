using System;
using System.Reflection;
using System.Web;

namespace PartTwo.Workflow.Infrastructure
{
    public class EventListModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            string[] events = {
                "BeginRequest", "AuthenticateRequest", "PostAuthenticateRequest",
                "AuthorizeRequest", "ResolveRequestCache", "PostResolveRequestCache",
                "MapRequestHandler", "PostMapRequestHandler", "AcquireRequestState",
                "PostRequestHandlerExecute", "ReleaseRequestState","PreSendRequestContent",
                "PostReleaseRequestState", "UpdateRequestCache", "LogRequest",
                "PostAcquireRequestState", "PreRequestHandlerExecute",
                "PostLogRequest", "EndRequest", "PreSendRequestHeaders",
                
            };

            MethodInfo methodinfo = GetType().GetMethod("HandleEvent");
            foreach (string name in events)
            {
                EventInfo eventInfo = app.GetType().GetEvent(name);

                eventInfo.AddEventHandler(app, Delegate.CreateDelegate(
                    eventInfo.EventHandlerType, this, methodinfo));
            }

            app.Error += (src, args) =>
            {
                System.Diagnostics.Debug.WriteLine("Event: Error");
            };
        }

        public void HandleEvent(object src, EventArgs args)
        {
            string name = HttpContext.Current.CurrentNotification.ToString();
            if (HttpContext.Current.IsPostNotification &&
                !HttpContext.Current.Request.CurrentExecutionFilePathExtension.Equals("css"))
            {
                name = "Post" + name;
            }

            if (name == "BeginRequest")
            {
                System.Diagnostics.Debug.WriteLine("------------------------");
            }
            System.Diagnostics.Debug.WriteLine("Event: {0}", new string[] { name });
        }
        public void Dispose() { }
    }
}