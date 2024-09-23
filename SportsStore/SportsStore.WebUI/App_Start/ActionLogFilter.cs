using SportsStore.Domain.Entities;
using SportsStore.Domain.Concrete;
using System;
using System.Web.Mvc;


namespace SportsStore.WebUI.ActionFilter {
    class ActionLogFilter : ActionFilterAttribute, IActionFilter {

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext) {

            using (EFDbContext myDB = new EFDbContext()) {

                ActionLog log = new ActionLog() {
                    Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    Action = filterContext.ActionDescriptor.ActionName,
                    HttpMethod = filterContext.RequestContext.HttpContext.Request.HttpMethod,
                    URL = filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri,
                    ActionDate = DateTime.Now,
                };

                myDB.ActionLogs.Add(log);
                myDB.SaveChanges();
                OnActionExecuting(filterContext);
            }
        }

    }
}
