using System;
using System.Data.Entity;

namespace PingYourPackage
{
    public interface IEntity
    {
        Guid Key { get; set; }
    }

    public class EntitiesContext : DbContext
    {
        public EntitiesContext() : base("PingYourPackage") { }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<UserInRole> UserInRoles { get; set; }
    }
}
