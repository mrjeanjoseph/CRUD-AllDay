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

            if (!userdetail.Email.ToLower().EndsWith("@dvc.com"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Only example.com email address are allowed.");
                result = new IdentityResult(errors);
            }

            return result;
        }
    }
}