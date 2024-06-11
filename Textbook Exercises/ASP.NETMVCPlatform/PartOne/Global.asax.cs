using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace PartOne
{
    public class MvcApplication : HttpApplication
    {
        public MvcApplication()
        {
            PostAcquireRequestState += (src, args) => CreateTimeStamp(); 
        }


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            CreateTimeStamp();
        }
        private void CreateTimeStamp()
        {
            string stamp = Context.Timestamp.ToLongTimeString();
            if (Context.Session != null)
                Session["request_timestamp"] = stamp;
            else Application["app_timestamp"] = stamp;
        }
        #region archived

        //public MvcApplication()
        //{
        //    beginrequest += recordevent;
        //    authenticaterequest += recordevent;
        //    postauthenticaterequest += recordevent;

        //    beginrequest += (src, args) => recordevent("beginrequest");
        //    authenticaterequest += (src, args) => recordevent("authenticaterequest");
        //    postauthenticaterequest += (src, args) => recordevent("postauthenticaterequest");
        //}

        //protected void Application_BeginRequest()
        //{
        //    RecordEvent("BeginRequest");
        //}

        //protected void Application_AuthenticateRequest()
        //{
        //    RecordEvent("AuthenticateRequest");
        //}

        //protected void Application_PostAuthenticateRequest()
        //{
        //    RecordEvent("PostAuthenticateRequest");
        //}

        private void RecordEventOld2(object src, EventArgs args)
        {
            List<string> eventList = Application["events"] as List<string>;

            if (eventList == null)
                Application["events"] = eventList = new List<string>();

            string name = Context.CurrentNotification.ToString();
            if (Context.IsPostNotification)
                name = "Post" + name;

            eventList.Add(name);
        }

        private void RecordEvent_old(string name)
        {
            List<string> eventList = Application["events"] as List<string>;

            if (eventList == null)
                Application["events"] = eventList = new List<string>();
            
            eventList.Add(name);            
        }

        #endregion
    }
}
