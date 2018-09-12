using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YGCGanpati.Startup))]
namespace YGCGanpati
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
