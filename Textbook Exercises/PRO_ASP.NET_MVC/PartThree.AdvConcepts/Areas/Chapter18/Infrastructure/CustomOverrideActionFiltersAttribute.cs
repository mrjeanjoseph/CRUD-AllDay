using System;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Chapter18.ApplyingFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, 
        Inherited = true, AllowMultiple = true)]
    public class CustomOverrideActionFiltersAttribute : FilterAttribute, IOverrideFilter
    {
        public Type FiltersToOverride
        {
            //get => typeof(IActionFilter);
            get { return typeof(IActionFilter); }
        }
    }
}