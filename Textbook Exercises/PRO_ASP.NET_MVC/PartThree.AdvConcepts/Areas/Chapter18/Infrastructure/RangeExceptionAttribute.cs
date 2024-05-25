using System;
using System.Web.Mvc;

namespace Chapter18.ApplyingFilters
{
    public class RangeExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext contextParam)
        {
            if (!contextParam.ExceptionHandled && contextParam.Exception is ArgumentOutOfRangeException)
            {
                //contextParam.Result = new RedirectResult("~/Content/RangeErrorPage.html");
                int filteredValue = (int)((ArgumentOutOfRangeException)contextParam.Exception).ActualValue;
                contextParam.Result = new ViewResult
                {
                    ViewName = "RangeError",
                    ViewData = new ViewDataDictionary<int>(filteredValue)
                };
                contextParam.ExceptionHandled = true;
            }
        }
    }
}