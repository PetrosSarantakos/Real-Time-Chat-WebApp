using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(BetterTeamsWebApp.Startup))]
namespace BetterTeamsWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}