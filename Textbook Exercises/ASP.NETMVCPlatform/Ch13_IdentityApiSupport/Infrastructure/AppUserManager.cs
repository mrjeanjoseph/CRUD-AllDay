using IdentityApiSupport.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace IdentityApiSupport.Infrastructure
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store) { }

        public static AppUserManager Create(
            IdentityFactoryOptions<AppUserManager> options, 
            IOwinContext context)
        {
            AppIdentityDBContext dBContext = context.Get<AppIdentityDBContext>();
            AppUserManager userManager = new AppUserManager(new UserStore<AppUser>(dBContext));

            userManager.PasswordValidator = new CustomPasswordValidator
            {
                RequiredLength = 6,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireNonLetterOrDigit = false,
            };

            userManager.UserValidator = new CustomUserValidator(userManager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true,
            };

            return userManager;
        }
    }
}