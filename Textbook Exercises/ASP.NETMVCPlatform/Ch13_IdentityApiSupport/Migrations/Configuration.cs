using IdentityApiSupport.Infrastructure;
using IdentityApiSupport.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;

namespace IdentityApiSupport.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<AppIdentityDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "IdentityApiSupport.Infrastructure.AppIdentityDBContext";
        }

        protected override void Seed(AppIdentityDBContext context)
        {
            AppUserManager userMgmt = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleMgmt = new AppRoleManager(new RoleStore<AppRole>(context));

            string roleName = "Administrators";
            string userName = "Admin";
            string passworld = "Granted100";
            string email = "admin@dvc.ht";

            if (!roleMgmt.RoleExists(roleName))
                roleMgmt.Create(new AppRole(roleName));

            AppUser user = userMgmt.FindByName(userName);
            if (user == null)
            {
                userMgmt.Create(new AppUser { UserName = userName, Email = email }, passworld);
                user = userMgmt.FindByName(userName);
            }

            if (!userMgmt.IsInRole(user.Id, roleName))
                userMgmt.AddToRole(user.Id, roleName);

            foreach (AppUser dbUser in userMgmt.Users)
            {
                if (dbUser.Country == Countries.None)
                    dbUser.SetCountryFromCity(dbUser.City);
            }

            context.SaveChanges();
        }
    }
}
