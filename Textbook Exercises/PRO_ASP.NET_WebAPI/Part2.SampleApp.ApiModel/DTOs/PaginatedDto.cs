using System.Collections.Generic;

namespace PingYourPackage.ApiModel
{
    public class PaginatedDto<TDto> : IPaginatedDto<TDto> where TDto : IDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalPageCount { get; set; }
        public int TotalCount { get; set; } // Looks like this one was missing
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        IEnumerable<TDto> IPaginatedDto<TDto>.Items => throw new System.NotImplementedException();

        public IEnumerable<TDto> Items;
    }
}
