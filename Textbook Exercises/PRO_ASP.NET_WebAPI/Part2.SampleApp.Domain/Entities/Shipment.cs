using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PingYourPackage.Domain
{
    public class Shipment : IEntity
    {
        [Key] public Guid Key { get; set; }
        public Guid AffliateKey { get; set; }
        public Guid ShipmentTypeKey { get; set; }

        public decimal Price { get; set; }

        [Required, StringLength(64)]
        public string ReceiverName { get; set; }

        [Required, StringLength(64)]
        public string ReceiverSurname { get; set; }

        [Required, StringLength(64)]
        public string ReceiverAddress{ get; set; }

        [Required, StringLength(64)]
        public string ReceiverZipCode{ get; set; }

        [Required, StringLength(64)]
        public string ReceiverCity{ get; set; }

        [Required, StringLength(64)]
        public string ReceiverCountry{ get; set; }

        [Required, StringLength(64)]
        public string ReceiverPhoneNumber{ get; set; }

        [Required, StringLength(256)]
        public string ReceiverEmailAddress{ get; set; }

        public DateTime CreatedOn { get; set; }

        public Affliliate Affliliate { get; set; }
        public ShipmentType ShipmentType { get; set; }

        public virtual ICollection<ShipmentState> ShipmentStates { get; set; }

        public Shipment()
        {
            ShipmentStates = new HashSet<ShipmentState>();
        }

    }
}
