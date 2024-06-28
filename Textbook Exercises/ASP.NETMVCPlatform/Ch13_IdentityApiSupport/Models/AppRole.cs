using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityApiSupport.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }

        public AppRole(string name) : this() { }
    }

}