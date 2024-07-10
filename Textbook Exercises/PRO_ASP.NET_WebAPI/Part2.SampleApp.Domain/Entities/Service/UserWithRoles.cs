using System.Collections.Generic;

namespace PingYourPackage.Domain
{
    public class UserWithRoles
    {
        public User User { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}