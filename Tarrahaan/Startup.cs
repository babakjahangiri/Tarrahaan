using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tarrahaan.Startup))]
namespace Tarrahaan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
