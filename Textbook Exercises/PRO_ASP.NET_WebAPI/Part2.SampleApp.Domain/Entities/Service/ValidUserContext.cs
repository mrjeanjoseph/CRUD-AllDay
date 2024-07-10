using System.Security.Principal;

namespace PingYourPackage.Domain
{

    public class ValidUserContext
    {
        #region documentation
            //Something is supposed to happen here.
            //Book did not provide this info
            //copied from GitHub
            //https://github.com/tugberkugurlu/PingYourPackage/blob/Edition-1/src/apps/PingYourPackage.Domain/Services/ValidUserContext.cs
        #endregion

        public IPrincipal Principal { get; set; }
        public UserWithRoles User { get; set; }
        public bool IsValid() => Principal != null;
    }
}

