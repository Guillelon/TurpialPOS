using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TurpialPOS.Startup))]
namespace TurpialPOS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
