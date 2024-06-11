using System.Web;

namespace CommonModules
{
    public class ModuleInfo : IHttpModule
    {

        public void Init(HttpApplication context)
        {
            context.EndRequest += (src, args) =>
            {
                HttpContext ctx = HttpContext.Current;
                ctx.Response.Write(string.Format(
                    "<div class='alert alert-success'>URL: {0} Status: {1}</div>",
                    ctx.Request.RawUrl, ctx.Response.StatusCode));
            };
        }

        public void Dispose()
        {
            // Will do something something soon here
        }

    }
}
