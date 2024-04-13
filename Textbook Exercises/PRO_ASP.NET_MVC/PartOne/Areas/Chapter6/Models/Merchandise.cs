using System;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace EssentialTools.Models {
    public class Merchandise {
        public int MerchandiseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public int IsAvailable { get; set; }
    }
}