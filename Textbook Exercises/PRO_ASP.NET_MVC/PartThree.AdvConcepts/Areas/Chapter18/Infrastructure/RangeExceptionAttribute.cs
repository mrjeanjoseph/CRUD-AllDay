using System;
using System.Web.Mvc;

namespace Chapter18.ApplyingFilters
{
    public class RangeExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is ArgumentOutOfRangeException)
            {
                //filterContext.Result = new RedirectResult("~/Content/RangeErrorPage.html");
                int filteredValue = (int)((ArgumentOutOfRangeException)filterContext.Exception).ActualValue;
                filterContext.Result = new ViewResult
                {
                    ViewName = "RangeError",
                    ViewData = new ViewDataDictionary<int>(filteredValue)
                };
                filterContext.ExceptionHandled = true;
            }
        }
    }
}