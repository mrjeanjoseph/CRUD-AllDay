using System;

namespace PingYourPackage.ApiModel
{
    public class ShipmentStateDto
    {
        public Guid Key { get; set; }
        public Guid ShipmentKey { get; set; }
        public ShipmentStatus ShipmentStatus { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}