using System.Collections.Generic;

namespace PingYourPackage.ApiModel
{
    public class PaginatedDto<TDto> : IPaginatedDto<TDto> where TDto : IDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalPageCount { get; set; }

        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public IEnumerable<TDto> Items;
    }
}
