using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DigitalMenu.Startup))]
namespace DigitalMenu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
