using System;
using System.Web.Security;

namespace SportsStore.Web.Infrastructure
{
    public class FormsAuthProvider : IAuthProvider
    {
        [Obsolete]
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)            
                FormsAuthentication.SetAuthCookie(username, false);
            
            return result;
        }
    }
}