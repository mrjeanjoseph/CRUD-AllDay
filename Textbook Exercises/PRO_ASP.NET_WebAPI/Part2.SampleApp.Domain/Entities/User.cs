using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PingYourPackage.Domain
{
    public class User : IEntity
    {
        [Key]
        public Guid Key { get; set; }

        [Required, StringLength(64)]
        public string FullLegalName { get; set; }

        [Required, StringLength(256)]
        public string EmailAddress { get; set; }

        [Required]
        public string HashedPassword { get; set; }
        [Required]
        public string Salt { get; set; }

        public bool IsLocked { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }

        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        public virtual Affliliate Affliliate { get; set; }

        public User()
        {
            UserInRoles = new HashSet<UserInRole>();
            Affliliate = new Affliliate();
        }
    }
}
