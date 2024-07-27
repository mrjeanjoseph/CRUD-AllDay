using System.Collections.Generic;

namespace PingYourPackage.ApiModel
{
    public interface IPaginatedDto<out TDto> where TDto : iDto 
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int PageCount { get; set; }
        int TotalPageCount { get; set; }

        int TotalCount { get; set; } // Looks like this one was missing
        bool HasNextPage { get; set; }
        bool HasPreviousPage { get; set; }

        IEnumerable<TDto> Items { get; }
    }
}
