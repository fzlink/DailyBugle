using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DailyBugle.Startup))]
namespace DailyBugle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
