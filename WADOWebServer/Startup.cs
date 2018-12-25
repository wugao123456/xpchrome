using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WADOWebServer.Startup))]
namespace WADOWebServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
