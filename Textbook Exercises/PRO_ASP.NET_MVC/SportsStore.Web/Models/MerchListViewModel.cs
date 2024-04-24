using SportsStore.Domain;
using System.Collections.Generic;

namespace SportsStore.Web.Models
{
    public class MerchListViewModel
    {
        public IEnumerable<Merchandise> Merchandises { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}