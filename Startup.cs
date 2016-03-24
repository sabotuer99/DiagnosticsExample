using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DiagnosticsExample.Startup))]
namespace DiagnosticsExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
