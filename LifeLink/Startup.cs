using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LifeLink.Startup))]
namespace LifeLink
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
