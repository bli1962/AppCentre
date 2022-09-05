using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mhcb.syd.DataService.Client.Startup))]
namespace mhcb.syd.DataService.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
