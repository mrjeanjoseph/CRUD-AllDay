using SportsStore.Domain;
using System.Collections.Generic;

namespace SportsStore.Web.Models
{
    public class MerchListViewModel
    {
        public IEnumerable<Merchandise> Merchandises { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }

    public class ProductVendorListViewModel
    {
        public IEnumerable<ProductVendor> ProductVendors { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }

    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}