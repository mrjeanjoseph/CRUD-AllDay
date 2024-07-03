using IdentityApiSupport.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace IdentityApiSupport
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder appbuiler)
        {
            appbuiler.CreatePerOwinContext<AppIdentityDBContext>(AppIdentityDBContext.Create);
            appbuiler.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            appbuiler.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);

            appbuiler.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });

            appbuiler.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            appbuiler.UseGoogleAuthentication(); // Nuget Package is outdated and Google maybe using two factor now
            // This code is not working

        }
    }
}
