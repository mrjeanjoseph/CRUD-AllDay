using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PingYourPackage.Domain
{
    public class Affliliate : IEntity
    {
        [Key] public Guid Key { get; set; }

        [Required, StringLength(64)]
        public string CompanyName { get; set; }

        [Required, StringLength(256)]
        public string PhisicalAddress { get; set; }

        [StringLength(16)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime CreatedOne { get; set; }

        [Required]
        public User User { get; set; }

        public virtual ICollection<Shipment> Shipments { get; set; }

        public Affliliate()
        {
            Shipments = new HashSet<Shipment>();
        }
    }
}
