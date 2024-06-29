using IdentityApiSupport.Models;
using Microsoft.AspNet.Identity;
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

    public class IdentityDBInit : DropCreateDatabaseIfModelChanges<AppIdentityDBContext>
    {
        protected override void Seed(AppIdentityDBContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(AppIdentityDBContext context)
        {
            AppUserManager userMgmt = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleMgmt = new AppRoleManager(new RoleStore<AppRole>(context));

            string roleName = "Administrators";
            string userName = "Admin";
            string password = "Granted100";
            string email = "admin@dvc.ht";

            if (!roleMgmt.RoleExists(roleName))
                roleMgmt.Create(new AppRole(roleName));

            AppUser user = userMgmt.FindByName(userName);
            if (user == null)
            {
                userMgmt.Create(new AppUser { UserName = userName, Email = email }, password);
                user = userMgmt.FindByName(userName);
            }

            if(!userMgmt.IsInRole(user.Id, roleName)) 
                userMgmt.AddToRole(user.Id, roleName);            
        }
    }
}