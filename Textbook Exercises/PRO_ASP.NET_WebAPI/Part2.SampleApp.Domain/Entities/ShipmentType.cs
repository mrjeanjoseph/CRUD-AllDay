using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PingYourPackage.Domain
{
    public class ShipmentType : IEntity
    {
        [Key] public Guid Key { get ; set ; }

        [Required, StringLength(64)]
        public string ShipmentName { get; set; }

        public decimal Price { get; set; }
        public DateTime CreatedOne { get; set; }

        public virtual ICollection<Shipment> Shipments { get; set; }

        public ShipmentType()
        {
            Shipments = new HashSet<Shipment>();
        }
    }

    public class ShipmentState : IEntity
    {
        [Key] public Guid Key { get; set; }
        public Guid ShipmentKey { get; set; }

        [Required]
        public ShipmentStatus ShipmentStatus { get; set; }
        public DateTime CreatedOn { get; set; }

        public Shipment Shipment { get; set; }
    }
}
