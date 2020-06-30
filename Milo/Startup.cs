using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Milo.Startup))]
namespace Milo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
