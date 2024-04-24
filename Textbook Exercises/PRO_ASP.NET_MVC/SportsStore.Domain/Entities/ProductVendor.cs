using System;

namespace SportsStore.Domain
{
    public class ProductVendor
    {
        public int ProductID { get; set; }
        public int StandardPrice { get; set; }
        public DateTime LastReceiptDate { get; set; }
    }
}