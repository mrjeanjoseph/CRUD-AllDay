using PingYourPackage.ApiModel;
using PingYourPackage.Domain;
using System.Collections.Generic;

namespace PingYourPackage.WebAPI
{
    internal static class PaginatedListExtensions
    {
        internal static PaginatedDto<TDto> ToPaginatedDto<TDto, TEntity>(
            this PaginatedList<TEntity> source, IEnumerable<TDto> items) where TDto : IDto
        {
            return new PaginatedDto<TDto>
            {
                PageIndex = source.PageIndex,
                PageSize = source.PageSize,

                TotalPageCount = source.TotalPageCount,
                HasNextPage = source.HasNextPage,
                HasPreviousPage = source.HasPreviousPage,
                Items = items
            };
        }
    }
}
