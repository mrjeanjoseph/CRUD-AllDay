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

            appbuiler.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}
