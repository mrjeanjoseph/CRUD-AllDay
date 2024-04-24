using System;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Domain
{
    public class ProductVendor
    {
        [Key] public int ProductID { get; set; }
        public int StandardPrice { get; set; }
        public DateTime LastReceiptDate { get; set; }
    }
}