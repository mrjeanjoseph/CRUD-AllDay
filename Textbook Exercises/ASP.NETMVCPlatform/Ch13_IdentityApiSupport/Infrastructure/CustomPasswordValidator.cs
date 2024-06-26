using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApiSupport.Infrastructure
{
    public class CustomPasswordValidator : PasswordValidator
    {
        public override async Task<IdentityResult> ValidateAsync(string passwordItem)
        { 
            IdentityResult result = await base.ValidateAsync(passwordItem);
            if (passwordItem.Contains("12345")){
                var errors = result.Errors.ToList();
                errors.Add("Passwords cannot contain numberic sequences");
                result = new IdentityResult(errors);
            }
            return result;
        }
    }
}