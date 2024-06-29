using IdentityApiSupport.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace IdentityApiSupport.Infrastructure
{
    public class AppIdentityDBContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDBContext() : base("IdentityConn") { }

        static AppIdentityDBContext() =>
            Database.SetInitializer<AppIdentityDBContext>(new IdentityDBInit());

        public static AppIdentityDBContext Create() => new AppIdentityDBContext();
    }

    public class IdentityDBInit : NullDatabaseInitializer<AppIdentityDBContext> { }
}