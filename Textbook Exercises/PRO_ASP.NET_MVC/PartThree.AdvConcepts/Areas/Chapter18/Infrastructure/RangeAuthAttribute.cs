using System.Web.Mvc;

namespace Chapter18.ApplyingFilters
{
    public class RangeAuthAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            throw new System.NotImplementedException();
        }
    }
}