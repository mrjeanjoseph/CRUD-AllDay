using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PingYourPackage.Domain
{
    public class Role : IEntity
    {
        [Key] public Guid Key { get; set; }

        [Required]
        public string Name { get; set; }


        public virtual ICollection<UserInRole> UserInRoles { get; set; }

        public Role()
        {
            UserInRoles = new HashSet<UserInRole>();
        }
    }
}
