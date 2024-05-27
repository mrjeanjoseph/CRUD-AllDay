using System.Web.Mvc;

namespace Chapter20.RazorPagesAndViewEngine.Infrastructure
{
    public class CustomLocationViewEngine : RazorViewEngine
    {
        public CustomLocationViewEngine()
        {
            ViewLocationFormats = new string[] {
                "~/Areas/Chapter20/Views/{1}/{0}.cshtml",
                "~/Areas/Chapter20/Views/Common/{0}.cshtml"
            };
        }
    }
}