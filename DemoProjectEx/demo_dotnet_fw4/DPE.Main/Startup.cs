using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DPE.Main.Startup))]

namespace DPE.Main
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
