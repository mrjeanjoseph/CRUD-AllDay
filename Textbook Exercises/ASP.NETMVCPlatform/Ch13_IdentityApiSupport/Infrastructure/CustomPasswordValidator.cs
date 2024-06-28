using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

    public static class IdentityHelpers
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {
            AppUserManager mgr = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.UserName);
        }
    }
}