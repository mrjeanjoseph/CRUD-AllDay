using IdentityApiSupport.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApiSupport.Infrastructure
{
    public class CustomUserValidator : UserValidator<AppUser>
    {
        public CustomUserValidator(UserManager<AppUser, string> manager) 
            : base(manager) { }

        public override async Task<IdentityResult> ValidateAsync(AppUser userdetail)
        {
            IdentityResult result = await base.ValidateAsync(userdetail);

            string devemails = userdetail.Email.ToLower();
            bool dvcdomains = devemails.EndsWith("@dvc.ht") || devemails.EndsWith("@dvc.westindes");
            if (!dvcdomains)
            {
                var errors = result.Errors.ToList();
                errors.Add("Only dvc.ht or dvc.westindes email addresses are allowed.");
                result = new IdentityResult(errors);
            }

            return result;
        }
    }
}