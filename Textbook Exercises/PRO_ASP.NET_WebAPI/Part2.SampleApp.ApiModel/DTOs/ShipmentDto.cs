using System;
using System.Collections.Generic;

namespace PingYourPackage.ApiModel
{
    public class ShipmentDto : IDto
    {
        public Guid Key { get; set; }
        public Guid AffliliateKey { get; set; }


        public decimal Price { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverSurname { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverZipCode { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverCountry { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string ReceiverEmailAddress { get; set; }
        public DateTime CreatedOn { get; set; }

        public ShipmentTypeDto ShipmentType { get; set; }

        public IEnumerable<ShipmentStateDto> ShipmentSates { get; set; }

    }
}
