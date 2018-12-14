using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(SignalR_Chat.Startup))]
namespace SignalR_Chat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}