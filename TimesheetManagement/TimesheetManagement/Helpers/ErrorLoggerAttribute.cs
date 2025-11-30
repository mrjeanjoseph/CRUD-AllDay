using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;

namespace TimesheetManagement.Helpers
{
    public class ErrorLoggerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            string strLogText = string.Empty;
            Exception ex = context.Exception;
            context.ExceptionHandled = true;
            strLogText += "Message ---\n{0}" + ex.Message;

            // Basic source hints
            if (ex.Source == ".Net SqlClient Data Provider")
            {
                strLogText += Environment.NewLine + "SqlClient Error ---\n{0}" + "Check Sql Error";
            }
            else if (ex.Source == "Microsoft.AspNetCore.Mvc")
            {
                strLogText += Environment.NewLine + ".Net Error ---\n{0}" + "Check MVC Code For Error";
            }

            strLogText += Environment.NewLine + "Source ---\n{0}" + ex.Source;
            strLogText += Environment.NewLine + "StackTrace ---\n{0}" + ex.StackTrace;
            strLogText += Environment.NewLine + "TargetSite ---\n{0}" + ex.TargetSite;
            if (ex.InnerException != null)
            {
                strLogText += Environment.NewLine + "Inner Exception is {0}" + ex.InnerException;
            }
            if (ex.HelpLink != null)
            {
                strLogText += Environment.NewLine + "HelpLink ---\n{0}" + ex.HelpLink;
            }

            string timestamp = DateTime.Now.ToString("d-MMMM-yyyy", new CultureInfo("en-GB"));
            string error_folder = ConfigurationManager.AppSettings["ErrorLogPath"] ?? "Logs";

            if (!Directory.Exists(error_folder))
            {
                Directory.CreateDirectory(error_folder);
            }

            using var log = File.Exists(string.Format(@"{0}\Log_{1}.txt", error_folder, timestamp))
                ? File.AppendText(string.Format(@"{0}\Log_{1}.txt", error_folder, timestamp))
                : new StreamWriter(string.Format(@"{0}\Log_{1}.txt", error_folder, timestamp));

            var controllerName = (string)context.RouteData.Values["controller"];
            var actionName = (string)context.RouteData.Values["action"];
            log.WriteLine(Environment.NewLine + DateTime.Now);
            log.WriteLine("------------------------------------------------------------------------------------------------");
            log.WriteLine("Controller Name :- " + controllerName);
            log.WriteLine("Action Method Name :- " + actionName);
            log.WriteLine("------------------------------------------------------------------------------------------------");
            log.WriteLine(strLogText);
            log.WriteLine();

            context.HttpContext.Session.Clear();
            context.Result = new ViewResult { ViewName = "Error" };
        }
    }
}