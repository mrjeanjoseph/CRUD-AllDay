using System.ComponentModel.DataAnnotations;

namespace PingYourPackage.ApiModel
{
    public abstract class ShipmentBaseRequestModel
    {

        [Required]
        public decimal? Price { get; set; }

        [Required, StringLength(64)]
        public string ReceiverName { get; set; }


        [Required, StringLength(64)]
        public string ReceiverSurname { get; set; }

        [Required, StringLength(64)]
        public string ReceiverAddress { get; set; }

        [Required, StringLength(64)]
        public string ReceiverZipCode { get; set; }

        [Required, StringLength(64)]
        public string ReceiverCity { get; set; }

        [Required, StringLength(64)]
        public string ReceiverCountry { get; set; }

        [Required, StringLength(64)]
        public string ReceiverTelephone { get; set; }

        [Required, EmailAddress,StringLength(256)]
        public string ReceiverEmail { get; set; }
    }
}
