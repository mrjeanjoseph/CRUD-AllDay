using System.ComponentModel.DataAnnotations;
using System;

namespace PingYourPackage.ApiModel
{
    public class ShipmentRequestModel : ShipmentBaseRequestModel
    {
        [Required]
        public Guid? AffiliateKey { get; set; }

        [Required]
        public Guid? ShipmentTypeKey { get; set; }
    }
}
