using System;
using System.Web;

namespace PartOne.Infrastructure
{
    public class DayOfWeekHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string day = DateTime.Now.DayOfWeek.ToString();

            if(context.Request.CurrentExecutionFilePathExtension == ".json")
            {
                context.Response.ContentType = "application/json";
                context.Response.Write(string.Format("{{\"day\": \"{0}\"}}", day));
            } else
            {
                context.Response.ContentType = "text/html";
                context.Response.Write(string.Format("<span>It is: {0}</span>", day));
            }
        }
        public bool IsReusable { get { return false; } }
    }
}